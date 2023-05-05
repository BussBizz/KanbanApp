﻿using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace KantineApp.BL.Services;

public class BaseService
{
    private HttpClient _httpClient;
    private string _host = "https://localhost:7126";

    public BaseService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T> GetData<T>(string apiPath)
    {
        try
        {
            var requestUri = GetUri(apiPath);

            var result = await _httpClient.GetAsync(requestUri);

            if (result.IsSuccessStatusCode)
            {
                var s = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(s);
            }

            throw new Exception(result.StatusCode.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<T> PostData<T>(T data, string apiPath)
    {
        try
        {
            var requestUri = GetUri(apiPath);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(requestUri, content);

            if (result.IsSuccessStatusCode)
            {
                var s = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(s);
            }

            throw new Exception(result.StatusCode.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<T> UpdateData<T>(T data, string apiPath)
    {
        try
        {
            var requestUri = GetUri(apiPath);

            HttpContent content =
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(requestUri, content);

            if (result.IsSuccessStatusCode)
            {
                var s = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(s);
            }

            throw new Exception(result.StatusCode.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<T> DeleteData<T>(string apiPath)
    {
        try
        {
            var requestUri = GetUri(apiPath);

            var result = await _httpClient.DeleteAsync(requestUri);

            if (result.IsSuccessStatusCode)
            {
                var s = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(s);
            }

            throw new Exception(result.StatusCode.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private Uri GetUri(string path)
    {
        return new Uri(_host + path);
    }
}