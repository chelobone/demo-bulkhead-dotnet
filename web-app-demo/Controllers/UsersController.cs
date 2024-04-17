using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_app_demo.Entidades;
using web_app_demo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_app_demo.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUser _user;
        private ILogger<UsersController> _logger;

        public UsersController(IUser user, ILogger<UsersController> logger)
        {
            this._user = user;
            this._logger = logger;
        }

        // POST api/values
        [HttpPost]
        public IResult Post([FromBody] User value)
        {
            _logger.LogInformation($"Procesando usuario {value.Nombre}");
            var result = _user.CreateUserAsync(value).Result;

            _logger.LogInformation("Traza de la ejecución del procedimiento almacenado");
            _logger.LogInformation($"{TraceExtension.TraceFormat(result.Trace as string)}");

            return result.Match(
                onSuccess: () => Results.Ok(result.Data),
                onFailure: error => Results.BadRequest(error));
        }
    }
}

