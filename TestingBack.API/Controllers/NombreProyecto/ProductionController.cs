using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using TestingBack.SERVICE.DTO.NombreProyecto;
using TestingBack.SERVICE.Service.NombreProyecto;

namespace TestingBack.API.Controllers.NombreProyecto
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductionController : ControllerBase
    {
        
        private readonly ProductService _productService;
        private readonly ProductCategoryService _productCategoryService;
        private readonly ProductSubcategoryService _productSubcategoryService;
        private readonly IMapper _mapper;

        public ProductionController(
            IMapper mapper, 
            ProductService productService, 
            ProductCategoryService productCategoryService,
            ProductSubcategoryService productSubcategoryService
            )
        {
            _mapper = mapper;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productSubcategoryService = productSubcategoryService;
        }

      
        [HttpGet("GetCategorias")]
    
        public async Task<IActionResult> GetCategorias()
        {
            ServiceResponse<List<CategorysDTO>> response = new ServiceResponse<List<CategorysDTO>>();
            response.Exito = false;
            try
            {
                var categorias = await _productCategoryService.GetCategorys();

                if (categorias.data == null)
                {
                    response.Mensaje = "No se ha encontrado ningun registro.";
                    return BadRequest(response);
                }

                response.Exito = true;
                response.Data = _mapper.Map<List<CategorysDTO>>(categorias.data);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message.ToString();
                // Agregar bitacora de errores
                return UnprocessableEntity(response);
            }
        }

    }
}
