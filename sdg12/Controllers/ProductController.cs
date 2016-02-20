using NHibernate;
using sdg12.Service.Handlers;
using sdg12.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sdg12.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISession nhSession;

        public ProductController(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public ActionResult List()
        {
            var userId = 1;

            var request = new GetProductsQuery()
            {
                UserId = userId
            };

            var handler = new GetProductsQueryHandler(nhSession);
            var result = handler.Handle(request);

            return View(result);
        }
    }
}