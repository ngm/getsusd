using MediatR;
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
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult List()
        {
            var userId = 1;

            var request = new GetProductsQuery()
            {
                UserId = userId
            };

            var result = mediator.Send(request);

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

            var result = mediator.Send(command);

            return RedirectToAction("List");
        }
    }
}