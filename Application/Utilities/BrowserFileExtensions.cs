using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.Utilities;

public static class BrowserFileExtensions
{
    public static async Task<(byte[] fileContent, string fileContentType)> GetFile(this IBrowserFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        var fileContentType = file.ContentType;
        var fileContent = await GetFileAsByteArrayAsync();

        async Task<byte[]> GetFileAsByteArrayAsync()
        {
            await using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        return (fileContent, fileContentType);
    }
}