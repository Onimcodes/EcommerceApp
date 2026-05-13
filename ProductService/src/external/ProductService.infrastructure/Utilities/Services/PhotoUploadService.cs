using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProductService.application.Common.ApplicationConfigOptions;
using ProductService.application.Common.Utilities.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.infrastructure.Utilities.Services
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly CloudinaryOptions _cloudinaryOptions;
        private readonly Cloudinary _cloudinary;
        public PhotoUploadService(IOptions<CloudinaryOptions> options)
        {
            _cloudinaryOptions = options.Value;
            var acc = new Account(
                _cloudinaryOptions.CloudName,
                _cloudinaryOptions.ApiKey,
                _cloudinaryOptions.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult;
        }

        public async Task<ImageUploadResult> AddImageAsync(string ImageString)
        {
            var uploadResult = new ImageUploadResult();
            if (!string.IsNullOrEmpty(ImageString))
            {
                var imageBytes = Convert.FromBase64String(ImageString);
                using var stream = new MemoryStream(imageBytes);
                var randomName = Guid.NewGuid().ToString();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(randomName, stream),
                    UniqueFilename = true,
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Overwrite = true

                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }

            return uploadResult;

        }
    }
}
