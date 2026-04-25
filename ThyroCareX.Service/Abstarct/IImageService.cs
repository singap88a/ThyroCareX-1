using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Service.Abstarct
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream fileStream, string originalFileName, string subFolder = "doctors");
        void DeleteImage(string path, string subFolder = "doctors");
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileUrl);
    }
}
