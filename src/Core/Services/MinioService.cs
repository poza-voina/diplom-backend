using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class MinioService : IMinioService
{
	private readonly IMinioClient _minioClient;
	private readonly string _bucketName;
	private readonly string _endpoint;

	public MinioService(IMinioClient minioClient, IConfiguration configuration)
	{
		_minioClient = minioClient;
		_bucketName = configuration["Minio:Bucket"] ?? throw new InvalidOperationException("Minio:Bucket not found");
		_endpoint = configuration["Minio:Endpoint"] ?? throw new InvalidOperationException("Minio:Endpoint not found");
	}

	private async Task EnsureBucketExists()
	{
		var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
		if (!exists)
		{
			await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
		}
	}

	public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
	{
		var uris = new List<string>();
		foreach (var file in files)
		{
			var uri = await UploadFileAsync(file);
			uris.Add(uri);
		}
		return uris;
	}

	public async Task<string> UploadFileAsync(IFormFile file)
	{
		var objectName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
		using var stream = file.OpenReadStream();

		await _minioClient.PutObjectAsync(new PutObjectArgs()
			.WithBucket(_bucketName)
			.WithObject(objectName)
			.WithStreamData(stream)
			.WithObjectSize(file.Length)
			.WithContentType(file.ContentType));

		return $"/{_bucketName}/{objectName}";
	}

	public async Task<string?> GetErrorsAsync()
	{
		try
		{
			var buckets = await _minioClient.ListBucketsAsync();
			if (buckets.Buckets == null)
			{
				return "Cannot access MinIO buckets";
			}
		}
		catch (MinioException ex)
		{
			return $"MinIO error: {ex.Message}";
		}

		return null;
	}
}
