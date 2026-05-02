using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.application.ProductModule.Commands.AddProducts;
using ProductService.application.ProductModule.Commands.Dtos;

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

    }
}
