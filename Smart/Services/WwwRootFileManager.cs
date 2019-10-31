using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Smart.Services
{
    public class WwwRootFileManager : IFileManager
    {
        private readonly string _wwwRootDirectory;

        public WwwRootFileManager(IHostingEnvironment hostingEnvironment)
        {
            _wwwRootDirectory = hostingEnvironment.WebRootPath + "\\files\\";
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            return await Task.Factory.StartNew(() => 
            {
                filePath = filePath.TrimStart('\\');
                filePath = _wwwRootDirectory + filePath;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                return false;
            });
        }

        public async Task<byte[]> GetFileAsync(string filePath)
        {
            return await Task.Factory.StartNew(() =>
            {
                filePath = filePath.TrimStart('\\');
                filePath = _wwwRootDirectory + filePath;

                if (File.Exists(filePath))
                {
                    return File.ReadAllBytes(filePath);
                }

                return null;
            });
        }

        public async Task<bool> SaveFileAsync(IFormFile file, string filePath)
        {
            return await await Task.Factory.StartNew(async () =>
             {
                 try
                 {
                     await DeleteFileAsync(filePath);

                     filePath = filePath.TrimStart('\\');
                     string directory = _wwwRootDirectory + filePath.Substring(0, filePath.LastIndexOf('\\'));

                     if (!Directory.Exists(directory))
                     {
                         Directory.CreateDirectory(directory);
                     }

                     filePath = _wwwRootDirectory + filePath;

                     using (var ms = new MemoryStream())
                     {
                         file.CopyTo(ms);
                         var bytes = ms.ToArray();

                         using (var fs = File.Create(filePath))
                         {
                             fs.Write(bytes, 0, bytes.Length);
                         }
                     }

                     return true;
                 }
                 catch (Exception ex)
                 {
                     return false;
                 }
             });
        }
    }
}
