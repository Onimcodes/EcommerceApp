using MediatR;
using ProductService.application.Common.Dtos;
using ProductService.application.Common.Utilities.Interfaces.Services;
using ProductService.application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Commands.AddProductImage
{
    public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, RequestResponse<string>>
    {
        private readonly IPhotoUploadService _photoUploadService;

        public AddProductImageCommandHandler(IPhotoUploadService photoUploadService)
        {
            _photoUploadService = photoUploadService;
        }



        public async Task<RequestResponse<string>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            var result  = await _photoUploadService.AddImageAsync(request.imagefile);

            return new RequestResponse<string>
            {
                ResponseCode = 200,
                ResponseData = result.Url.ToString(),
                ResponseMessage = "Image uploaded successfully"
            };

        }
    }
}
