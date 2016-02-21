using MediatR;
using NHibernate;
using sdg12.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sdg12.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public virtual ActionResult Index()
        {
            //var userId = mediator.Send(new SeedDataCommand());
            return RedirectToAction("List", "Product");
        }
        public virtual ActionResult About()
        {
            return View();
        }
    }
}