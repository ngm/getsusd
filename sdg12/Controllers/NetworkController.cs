using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sdg12.Controllers
{
    public partial class NetworkController : Controller
    {
        // GET: Network
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}