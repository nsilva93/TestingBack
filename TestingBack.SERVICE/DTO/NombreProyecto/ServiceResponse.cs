using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBack.SERVICE.DTO.NombreProyecto
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
    }
}
