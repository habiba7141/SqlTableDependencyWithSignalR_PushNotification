using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_PushNotification.Models;

namespace SignalR_PushNotification.Controllers
{
    public class NotificationController : Controller
    {
       
        

      
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

     
    }
}
