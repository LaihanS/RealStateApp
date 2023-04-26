using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RealStateApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly Lazy<IMediator> _mediator;

        protected IMediator Mediator => _mediator.Value;

        protected BaseApiController(IServiceProvider services)
        {
            _mediator = new Lazy<IMediator>(() => services.GetRequiredService<IMediator>());
        }
    }
}
