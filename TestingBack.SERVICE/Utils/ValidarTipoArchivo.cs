using Microsoft.AspNetCore.Http;

namespace TestingBack.SERVICE.Lib
{
    public class ValidarTipoArchivo
    {
        public bool ValidarTipo(IFormFileCollection images)
        {
            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".pdf" };

            for (int i = 0; i < images.Count; i++)
            {
                string ext = images[i].FileName.Substring(images[i].FileName.LastIndexOf('.'));
                string extension = ext.ToLower();
                if (!AllowedFileExtensions.Contains(extension))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
