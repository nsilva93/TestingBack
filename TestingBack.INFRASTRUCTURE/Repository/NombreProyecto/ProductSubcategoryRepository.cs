using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBack.CORE.Interfaces.NombreProyecto;
using TestingBack.CORE.Models.NombreProyecto;
using TestingBack.INFRASTRUCTURE.Context;

namespace TestingBack.INFRASTRUCTURE.Repository.NombreProyecto
{
    public class ProductSubcategoryRepository : GenericRepository<ProductSubcategory>, IProductSubcategoryRepository
    {
        public ProductSubcategoryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
