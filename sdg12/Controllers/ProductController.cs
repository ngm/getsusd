using NHibernate;
using sdg12.Models;
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

        [HttpPost]
        public ActionResult Add(ProductInputModel productInputs)
        {
            var userId = 1;

            var command = new AddProductCommand
            {
                UserId = userId,
                ProductName = productInputs.ProductName,
                ProductNotes = productInputs.ProductNotes
            };

            var handler = new AddProductCommandHandler(nhSession);
            var result = handler.Handle(command);

            return RedirectToAction("List");
        }
    }
}