using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.application.ProductModule.Commands.AddProductImage;
using ProductService.application.ProductModule.Commands.AddProducts;
using ProductService.application.ProductModule.Commands.Dtos;
using ProductService.application.ProductModule.Queries.GetAllProducts;

namespace ProductService.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPost]

        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var command = new AddProductCommand(request);
            var commandResponse = await _sender.Send(command);
            return StatusCode(commandResponse.ResponseCode, commandResponse);
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var queryResponse = await _sender.Send(query);
            return StatusCode(queryResponse.ResponseCode, queryResponse);
        }


        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadProductImage([FromForm] IFormFile request)
        {
            var command = new AddProductImageCommand(request);
            var commandResponse = await _sender.Send(command);
            return StatusCode(commandResponse.ResponseCode, commandResponse);

        }
    }
}
