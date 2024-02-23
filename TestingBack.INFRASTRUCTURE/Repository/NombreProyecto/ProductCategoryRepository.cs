using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBack.CORE.Interfaces.NombreProyecto;
using TestingBack.CORE.Models;
using TestingBack.CORE.Models.NombreProyecto;
using TestingBack.INFRASTRUCTURE.Context;
using TestingBack.SERVICE.DTO.NombreProyecto;

namespace TestingBack.INFRASTRUCTURE.Repository.NombreProyecto
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Reply> GetCategorys()
        {
            Reply oR = new Reply();

            var query = @"
        SELECT DISTINCT pc.Name
        FROM Production.ProductCategory pc
        INNER JOIN Production.ProductSubcategory psc ON pc.ProductCategoryID = psc.ProductCategoryID
        INNER JOIN Production.Product p ON psc.ProductSubcategoryID = p.ProductSubcategoryID";

            var categorias = await _context.ProductCategory
                                            .FromSqlRaw(query)
                                            .Select(pc => new { CategoryName = pc.Name })
                                            .ToListAsync();

            if (categorias != null && categorias.Any()) // Verificar si hay categorías encontradas
            {
                List<CategorysDTO> listaCategorias = new List<CategorysDTO>();

                foreach (var categoria in categorias)
                {
                    CategorysDTO categoriaDTO = new CategorysDTO
                    {
                        name = categoria.CategoryName,  // Reemplazar con la propiedad correcta
                    };

                    listaCategorias.Add(categoriaDTO);
                }

                oR.data = listaCategorias; // Asignar la lista de DTOs
                oR.message = "Successful";
                oR.result = 1;
                return oR;
            }
            else
            {
                oR.message = "No se encuentra información";
                return oR;
            }
        }

    }
}
