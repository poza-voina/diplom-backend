using System;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
[Route("api/attachments")]
public class AttachmentsController(IMinioService minioService, IRepository<Attachment> attachmentRepository) : ControllerBase
{
	[HttpPost("cuepoints/upload")]
	public async Task<IResult> UploadAttachments(
		[FromForm] List<IFormFile> files,
		[FromForm] IEnumerable<long> cuePointIds)
	{
		if (files == null || files.Count == 0)
			return Results.BadRequest("Файлы не переданы.");

		if (cuePointIds == null || !cuePointIds.Any())
			return Results.BadRequest("Не указаны идентификаторы ключевых точек.");

		// Проверяем, что количество файлов совпадает с количеством ключевых точек
		if (files.Count != cuePointIds.Count())
			return Results.BadRequest("Количество файлов не совпадает с количеством ключевых точек.");

		// Загружаем файлы в Minio и получаем их URI
		var uris = await minioService.UploadFilesAsync(files);

		var attachments = new List<Attachment>();

		foreach (var (uri, cuePointId) in uris.Zip(cuePointIds, (uri, cuePointId) => (uri, cuePointId)))
		{
			var attachment = new Attachment
			{
				Uri = uri,
				CuePointId = cuePointId,
				FileName = files[uris.IndexOf(uri)].FileName
			};

			attachments.Add(attachment);
		}

		await UpdateOrCreateRangeCuepointAttachemntAsync(attachments, cuePointIds);

		return Results.Ok(attachments);
	}

	[HttpPost("route/upload")]
	public async Task<IResult> UploadRouteAttachment([FromForm] UploadAttachmentRequest request)
	{
		var uri = await minioService.UploadFileAsync(request.file);

		var attachment = new Attachment
		{
			Uri = uri,
			RouteId = request.routeId,
			FileName = request.file.FileName
		};

		await UpdateOrCreateRouteAttachmentAsync(attachment, request.routeId);
		return Results.Ok(attachment);
	}

	private async Task UpdateOrCreateRangeCuepointAttachemntAsync(IEnumerable<Attachment> attachments, IEnumerable<long> cuePointsIds)
	{
		var existing = await attachmentRepository
			.Items
			.Where(x => x.CuePointId != null)
			.Where(x => cuePointsIds.Contains(x.CuePointId!.Value)).ToListAsync();

		var difference = attachments.Except(existing, new AttachmentCuePointComparer()).ToList();


		await attachmentRepository.UpdateRangeAsync(existing);
		await attachmentRepository.CreateRangeAsync(difference);
	}

	private async Task UpdateOrCreateRouteAttachmentAsync(Attachment attachment, long routeId)
	{
		var existing = await attachmentRepository.Items
			.FirstOrDefaultAsync(x => x.RouteId == routeId);

		if (existing is null)
		{
			await attachmentRepository.CreateAsync(attachment);
		}
		else
		{
			await attachmentRepository.UpdateAsync(attachment);
		}
	}

	/// <summary>
	/// Получить все аттачменты для указанных ключевых точек.
	/// </summary>
	[HttpGet("cuepoints")]
	public async Task<IResult> GetAttachmentsByCuePoints([FromQuery] IEnumerable<long> cuePointIds)
	{
		if (cuePointIds == null || !cuePointIds.Any())
		{
			return Results.BadRequest("Не указаны идентификаторы ключевых точек.");
		}

		var attachments = await attachmentRepository.Items
			.Where(x => x.CuePointId != null && cuePointIds.Contains(x.CuePointId.Value))
			.ToListAsync();

		return Results.Ok(attachments);
	}

	[HttpGet("route/{routeId:long}")]
	public async Task<IResult> GetAttachmentByRoute([FromRoute] long routeId)
	{
		var attachment = await attachmentRepository.Items.FirstOrDefaultAsync(x => x.RouteId == routeId);
		return Results.Ok(attachment);
	}

	[HttpGet("route")]
	public async Task<IResult> GetAttachmentsByRoutes([FromQuery] IEnumerable<long> routeIds)
	{
		var attachment = await attachmentRepository.Items
			.Where(x => x.RouteId != null)
			.Where(x => routeIds.Contains(x.RouteId!.Value))
			.ToListAsync();
		return Results.Ok(attachment);
	}
}


public class AttachmentCuePointComparer : IEqualityComparer<Attachment>
{
	public bool Equals(Attachment x, Attachment y)
	{
		if (x == null || y == null) return false;
		return x.CuePointId == y.CuePointId;
	}

	public int GetHashCode(Attachment obj)
	{
		return obj.CuePointId.HasValue ? obj.CuePointId.Value.GetHashCode() : 0;
	}
}