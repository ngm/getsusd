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
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("list")]
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
        [Route("add")]
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


        [Route("{productId}/view")]
        public ActionResult View(int productId)
        {
            var userId = 1;

            var request = new GetProductsQuery()
            {
                UserId = userId,
                ProductId = productId
            };

            var result = mediator.Send(request);

            return View(result.Products.First());
        }

        [HttpPost]
        [Route("{productId}/edit")]
        public ActionResult Edit(int productId, ProductInputModel productInputs)
        {
            var userId = 1;

            var command = new EditProductCommand
            {
                ProductId = productInputs.ProductId,
                UserId = userId,
                ProductName = productInputs.ProductName,
                ProductNotes = productInputs.ProductNotes
            };

            var result = mediator.Send(command);

            return RedirectToAction("View", new { productId = productInputs.ProductId });
        }

        [HttpPost]
        [Route("{productId}/addtag")]
        public ActionResult AddTag(int productId, string tagName)
        {
            var userId = 1;

            var command = new AddTagToProductCommand
            {
                ProductId = productId,
                TagName = tagName,
                UserId = userId
            };

            var result = mediator.Send(command);

            return RedirectToAction("View", new { productId = productId });
        }
    }
}