using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBack.CORE.Interfaces;
using TestingBack.CORE.Models;
using TestingBack.SERVICE.DTO.NombreProyecto;

namespace TestingBack.SERVICE.Service.NombreProyecto
{
    public class ProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Reply> GetCategorys()
        {
            return  await _unitOfWork.ProductsCategory.GetCategorys();

            
        }
    }
}
