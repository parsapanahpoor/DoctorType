using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorType.Application.Convertors;
using DoctorType.Application.Security;
using DoctorType.Application.StaticTools;
using DoctorType.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DoctorType.Application.Extensions
{
    public static class ChunkFileUploadExtensions
    {

        public static string? AddChunkFileToServer(this IFormFile? file, string chunkPath, string originalPath, List<string>? validFormats = null)
        {
            if (file != null)
            {
                var fileFormat = Path.GetExtension(file.FileName.Split(".part")[0]);

                if (validFormats != null && validFormats.Any())
                {
                    if (validFormats.All(s => !s.Equals(fileFormat)))
                    {
                        return null;
                    }
                }

                var stream = file.OpenReadStream();
                var fileName = Path.GetFileName(file.FileName).Replace(" ", "-");

                if (!Directory.Exists(chunkPath))
                {
                    Directory.CreateDirectory(chunkPath);
                }

                var finalPath = Path.Combine(chunkPath, fileName);

                try
                {
                    if (System.IO.File.Exists(finalPath))
                    {
                        return null;
                    }

                    using (var fileStream = System.IO.File.Create(finalPath))
                    {
                        stream.CopyTo(fileStream);
                    }

                    var fileMerger = new FileMerger();

                    var finalName = fileMerger.MergeFile(finalPath, originalPath);

                    if (!string.IsNullOrEmpty(finalName))
                    {
                        return finalName;
                    }

                    return string.Empty;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

    }
}
