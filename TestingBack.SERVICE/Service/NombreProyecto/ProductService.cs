using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingBack.CORE.Interfaces;
using TestingBack.SERVICE.DTO.NombreProyecto;

namespace TestingBack.SERVICE.Service.NombreProyecto
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
    }
}
