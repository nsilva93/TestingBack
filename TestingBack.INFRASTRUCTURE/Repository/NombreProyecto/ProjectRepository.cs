using Microsoft.Extensions.Logging;
using TestingBack.CORE.Interfaces.NombreProyecto;
using TestingBack.CORE.Models;
using TestingBack.CORE.Models.NombreProyecto;
using TestingBack.INFRASTRUCTURE.Context;

namespace TestingBack.INFRASTRUCTURE.Repository.NombreProyecto
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
