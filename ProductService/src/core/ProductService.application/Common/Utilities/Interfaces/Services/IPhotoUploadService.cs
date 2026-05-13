using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.Common.Utilities.Interfaces.Services
{
    public interface IPhotoUploadService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<ImageUploadResult> AddImageAsync(string ImageString);
    }
}
