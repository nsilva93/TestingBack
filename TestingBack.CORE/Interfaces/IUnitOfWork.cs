using Microsoft.EntityFrameworkCore.Storage;
using TestingBack.CORE.Interfaces.NombreProyecto;

namespace TestingBack.CORE.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        IProductRepository Products { get; }

        IProductCategoryRepository ProductsCategory { get; }

        IProductSubcategoryRepository ProductsSubcategory { get; }

        Task<int> CompletedAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        DateTime DateTime_TimeZone_America_MX(); //Setea la fecha en la zona horaria del centro de méxico
        string RemoveDiacritics(string text); //Elimina los acentos en la cadena
    }
}
