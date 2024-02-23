using Microsoft.AspNetCore.Mvc;
using TestingBack.SERVICE.DTO.NombreProyecto;
using TestingBack.SERVICE.Service.NombreProyecto;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace TestingBack.API.Controllers.NombreProyecto
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [SwaggerTag("Endpoint para administración de información de los Servicios. Creación, lectura, actualización y baja.")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _service;
        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        /// <summary>Obtener todos los registros.</summary>
        /// <remarks>
        /// Obtiene todos los Servicios que están registrados.
        /// </remarks>
        /// <returns>
        /// Regresa un listado de todos los Servicios encontrados, un código de resultado, mensaje.
        /// </returns>
        /// <response code="200">Regresa información encontrada.</response>
        /// <response code="401">No tiene permisos de acceso.</response>
        [HttpGet("GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProjectDTO))]
        public async Task<IActionResult> Index()
        {
            var project = await _service.GetProjectAsync();
            return Ok(project);
        }
    }
}
