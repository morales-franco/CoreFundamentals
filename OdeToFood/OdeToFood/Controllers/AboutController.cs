using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    //Ideal para rutas especiales para controllers especificos
    //covnenciones o parametros especificos
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
 
        public string Phone()
        {
            return "15-969-84";
        }

        public string Adress()
        {
            return "ARGENTINA";
        }
    }
}
