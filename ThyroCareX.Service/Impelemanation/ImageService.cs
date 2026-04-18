using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Service.Abstarct;
using static System.Net.Mime.MediaTypeNames;
using SkiaSharp;


namespace ThyroCareX.Service.Impelemanation
{
    public class ImageService:IImageService
    {
        private readonly string _baseRootPath;
        private readonly string _wwwroot;

        public ImageService(string rootPath)
        {
            _wwwroot = rootPath;
            _baseRootPath = Path.Combine(rootPath, "uploads");

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

    }
}
