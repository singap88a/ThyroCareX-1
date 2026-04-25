using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Service.Abstarct;
using static System.Net.Mime.MediaTypeNames;


namespace ThyroCareX.Service.Impelemanation
{
    public class ImageService:IImageService
    {
        private readonly string _baseRootPath;
        private readonly string _wwwroot;
        private readonly IWebHostEnvironment _env;


        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _baseRootPath = Path.Combine(_wwwroot, "uploads");

            if (!Directory.Exists(_baseRootPath))
                Directory.CreateDirectory(_baseRootPath);
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string originalFileName, string subFolder = "doctors")
        {
            if (fileStream == null || fileStream.Length == 0)
                return null;

            var targetFolder = Path.Combine(_baseRootPath, subFolder);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            // توليد اسم جديد
            var fileName = $"{Guid.NewGuid():N}.webp";
            var filePath = Path.Combine(targetFolder, fileName);

            // استخدم SkiaSharp لتحويل الصورة مباشرة إلى WebP
            using var skBitmap = SKBitmap.Decode(fileStream);
            if (skBitmap == null)
                return null;

            using var image = SKImage.FromBitmap(skBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Webp, 80); // 80% جودة عالية مع ضغط ممتاز

            // كتابة مباشرة للملف باستخدام FileStream
            using var output = File.OpenWrite(filePath);
             data.SaveTo(output);

            // إعادة المسار النسبي
            return Path.Combine("uploads", subFolder, fileName).Replace("\\", "/");
        }
        public void DeleteImage(string path, string subFolder = "doctors")
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            var fileName = Path.GetFileName(path);
            var fullPath = Path.Combine(_wwwroot, "uploads", subFolder, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required");

            var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{fileName}";
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;

            try
            {
                var relativePath = fileUrl.TrimStart('/');
                var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", relativePath);

                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }

    }
}
