using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(File))]
    public class File
    {
        public int FileId { get; set; }
        public int StudentId { get; set; }
        public FileTypeEnum FileTypeId { get; set; }
        [Required]
        public byte[] ByteData { get; set; }
        [Required]
        [MaxLength(256)]
        public string FileName { get; set; }

        public virtual Student Student { get; set; }
        public virtual FileType FileType { get; set; }

        public static async Task<byte[]> SerializeFileAsync(IFormFile file)
        {
            return await Task.Factory.StartNew(() =>
            {
                var ms = new MemoryStream();
                file.CopyTo(ms);
                return ms.ToArray();
            });
        }
    }
}
