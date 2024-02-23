using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBack.SERVICE.DTO.NombreProyecto
{
    [Keyless]
    [NotMapped]
    public class CategorysDTO
    {
        public string name { get; set; }
    }
}
