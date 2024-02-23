﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBack.CORE.Models;
using TestingBack.CORE.Models.NombreProyecto;

namespace TestingBack.CORE.Interfaces.NombreProyecto
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
         Task<Reply> GetCategorys();
    }
}
