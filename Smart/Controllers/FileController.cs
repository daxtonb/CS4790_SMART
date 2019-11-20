using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(int fileId)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.FileId == fileId);

            if(file == null)
            {
                return NotFound();
            }

            return File(file.ByteData, "application/octet-stream", file.FileName);
        }
    }
}