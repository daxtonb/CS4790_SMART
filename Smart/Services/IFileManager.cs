using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Services
{
    public interface IFileManager
    {
        Task<bool> SaveFileAsync(IFormFile file, string filePath);
        Task<byte[]> GetFileAsync(string filePath);
        Task<bool> DeleteFileAsync(string filePath);
    }
}
