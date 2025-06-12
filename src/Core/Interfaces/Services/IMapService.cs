using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using Core.Dto;
using Microsoft.Extensions.Configuration;

namespace Core.Interfaces.Services;

public interface IMapService
{
	Task<AddressDto> GetAddressWithCoordsAsync(AddressDto dto);
}


public class MapService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : IMapService
{
	public async Task<AddressDto> GetAddressWithCoordsAsync(AddressDto dto)
	{
		var section = configuration.GetSection("YandexMaps") ?? throw new InvalidOperationException("Секция не найдена");
		var apikey = section["ApiKey"] ?? throw new InvalidOperationException("Ключ не найден");
		var geocoderUrl = section["GeocoderUrl"] ?? throw new InvalidOperationException("Адрес геокодера не найден");

		var handler = new HttpClientHandler
		{
			SslProtocols = System.Security.Authentication.SslProtocols.Tls12
		};

		var client = GetHttpClient(geocoderUrl);
		string request = GenerateGeoCoderQuery(dto, apikey);

		var response = await client.GetAsync(request);
		string jsonResponse = await response.Content.ReadAsStringAsync();

		ParseGeoCoderResult(dto, jsonResponse);

		return dto;
	}

	private static string GenerateGeoCoderQuery(AddressDto dto, string apikey)
	{
		var query = HttpUtility.ParseQueryString(string.Empty);
		query["apikey"] = apikey;
		query["geocode"] = dto.Address;
		query["format"] = "json";

		return "?" + query.ToString();
	}

	private static void ParseGeoCoderResult(AddressDto dto, string jsonResponse)
	{
		using JsonDocument document = JsonDocument.Parse(jsonResponse);
		var root = document.RootElement;

		var baseElement = root
			.GetProperty("response")
			.GetProperty("GeoObjectCollection")
			.GetProperty("featureMember")[0]
			.GetProperty("GeoObject");

		var addressElement = baseElement
			.GetProperty("metaDataProperty")
			.GetProperty("GeocoderMetaData")
			.GetProperty("text");

		var pointElement = baseElement
			.GetProperty("Point")
			.GetProperty("pos");


		string[] coords = pointElement.GetString()?.Split(' ') ?? throw new InvalidOperationException("");

		dto.Address = addressElement.GetString() ?? dto.Address;
		dto.Longitude = double.Parse(coords[0], CultureInfo.InvariantCulture);
		dto.Latitude = double.Parse(coords[1], CultureInfo.InvariantCulture);
	}

	private HttpClient GetHttpClient(string baseUrl)
	{
		var client = httpClientFactory.CreateClient("Geocoder");
		client.BaseAddress = new Uri(baseUrl);
		return client;
	}
}