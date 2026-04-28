using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ThyroCareX.Core.Dto.FnacAIResponse;
using ThyroCareX.Core.Dto.ImageAIResponse;
using ThyroCareX.Data.Healpers.ClinicalAI;
using ThyroCareX.Data.Healpers.ClinicalAIResponse;
using ThyroCareX.Service.Abstarct;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace ThyroCareX.Service.Impelemanation
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AIService(HttpClient httpClient, IConfiguration configuration, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _env = env;

            var apiKey = _configuration["AISettings:ApiKey"];
            _httpClient.DefaultRequestHeaders.Add("X-AI-Service-Key", apiKey);
        }
        public async Task<ImageAIResponse> PredictImageAsync(string imagePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", imagePath.TrimStart('/'));
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Image file not found at: {fullPath}");

            using var form = new MultipartFormDataContent();
            var fileBytes = await File.ReadAllBytesAsync(fullPath);
            var byteContent = new ByteArrayContent(fileBytes);
            
            // Set Content-Type
            var extension = Path.GetExtension(fullPath).ToLower();
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(
                extension == ".png" ? "image/png" : "image/jpeg"
            );

            form.Add(byteContent, "file", Path.GetFileName(fullPath));

            var response = await _httpClient.PostAsync(
                "https://amer003100-thyraxcdss.hf.space/image/predict",
                form
            );

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"AI Service Image Predict Error ({response.StatusCode}): {errorBody}");
            }

            return await response.Content.ReadFromJsonAsync<ImageAIResponse>();
        }
        public async Task<ClinicalAIResponse> AssessClinicalAsync(ClinicalRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://amer003100-thyraxcdss.hf.space/clinical/assess",
                request
            );

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"AI Service Clinical Assess Error ({response.StatusCode}): {errorBody}");
            }

            return await response.Content.ReadFromJsonAsync<ClinicalAIResponse>();
        }

        public async Task<FnacAIResponse> PredictFnacAsync(string imagePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", imagePath.TrimStart('/'));
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"FNAC image file not found at: {fullPath}");

            using var form = new MultipartFormDataContent();
            var fileBytes = await File.ReadAllBytesAsync(fullPath);
            var byteContent = new ByteArrayContent(fileBytes);
            
            var extension = Path.GetExtension(fullPath).ToLower();
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(
                extension == ".png" ? "image/png" : "image/jpeg"
            );

            form.Add(byteContent, "file", Path.GetFileName(fullPath));

            var response = await _httpClient.PostAsync(
                "https://amer003100-thyraxcdss.hf.space/fnac/predict",
                form
            );

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"AI Service FNAC Predict Error ({response.StatusCode}): {errorBody}");
            }

            return await response.Content.ReadFromJsonAsync<FnacAIResponse>();
        }
        public async Task<bool> ValidateUltrasoundAsync(string imagePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", imagePath.TrimStart('/'));

            using var form = new MultipartFormDataContent();
            var fileBytes = await File.ReadAllBytesAsync(fullPath);
            var byteContent = new ByteArrayContent(fileBytes);

            var extension = Path.GetExtension(fullPath).ToLower();
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(
                extension == ".png" ? "image/png" : "image/jpeg"
            );

            form.Add(byteContent, "file", Path.GetFileName(fullPath));

            var response = await _httpClient.PostAsync(
                "https://amer003100-thyraxcdss.hf.space/image/validate",
                form
            );

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<dynamic>();

            return result?.is_ultrasound == true;
        }

    }
}
