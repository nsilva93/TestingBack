using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TestingBack.CORE.Interfaces;
using TestingBack.CORE.Interfaces.NombreProyecto;
using TestingBack.INFRASTRUCTURE.Context;
using TestingBack.INFRASTRUCTURE.Repository.NombreProyecto;
using System.Globalization;
using System.Text;

namespace TestingBack.INFRASTRUCTURE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public IProjectRepository Projects { get; private set; }

        public IProductRepository Products { get; private set; }

        public IProductCategoryRepository ProductsCategory { get; private set; }

        public IProductSubcategoryRepository ProductsSubcategory { get; private set; }


        public UnitOfWork(
            ApplicationDbContext context,
            ILoggerFactory logger
            )
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Projects = new ProjectRepository(_context, _logger);
            Products = new ProductRepository(_context, _logger);
            ProductsCategory = new ProductCategoryRepository(_context, _logger);
            ProductsSubcategory = new ProductSubcategoryRepository(_context, _logger);
        }

        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public DateTime DateTime_TimeZone_America_MX() //Setea la fecha en la zona horaria del centro de méxico
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
            DateTime fechaActual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);

            return fechaActual;
        }

        public string RemoveDiacritics(string text)  //Elimina los acentos en la cadena
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
