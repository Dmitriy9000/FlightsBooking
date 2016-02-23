using System.Web.Mvc;

namespace PhaseTicket.Controllers
{
    public class FeaturedController : Controller
    {
        public ActionResult OK(string message = null)
        {
            return Json(new 
            {
                Success = true,
                Message = message
            });
        }

        public ActionResult Error(string message = null)
        {
            return Json(new
            {
                Success = false, 
                Message = message
            });
        }
    }
}