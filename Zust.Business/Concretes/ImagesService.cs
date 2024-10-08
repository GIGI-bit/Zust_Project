using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using System.IO;



namespace Zust.Business.Concretes
{
    public class ImagesService : IImageService
    {
       
        public async Task<string> SaveFile(IFormFile file,string folderName)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", folderName);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                var filePath = Path.Combine(fileDirectory, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"~/assets/images/{folderName}/" + fileName;
            }

            return null;
        }
    }
}
