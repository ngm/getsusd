using MediatR;
using NHibernate;
using sdg12.Models;
using sdg12.Service.Handlers;
using sdg12.Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace sdg12.Controllers
{
    [Authorize]
    [RoutePrefix("product")]
    public partial class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public int CurrentUserId()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            return Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [Route("list")]
        public virtual ActionResult List()
        {
            var request = new GetProductsQuery()
            {
                UserId = CurrentUserId()
            };

            var result = mediator.Send(request);

            return View(result);
        }

        [HttpPost]
        [Route("add")]
        public virtual ActionResult Add(ProductInputModel productInputs)
        {
            var userId = 1;

            var command = new AddProductCommand
            {
                UserId = CurrentUserId(),
                ProductName = productInputs.ProductName,
                ProductNotes = productInputs.ProductNotes
            };

            var result = mediator.Send(command);

            return RedirectToAction("List");
        }


        [Route("{productId}/view")]
        public virtual ActionResult View(int productId)
        {
            var request = new GetProductsQuery()
            {
                UserId = CurrentUserId(),
                ProductId = productId
            };

            var result = mediator.Send(request);

            return View(result.Products.First());
        }

        [HttpPost]
        [Route("{productId}/edit")]
        public virtual ActionResult Edit(int productId, ProductInputModel productInputs)
        {
            var userId = 1;

            var command = new EditProductCommand
            {
                ProductId = productInputs.ProductId,
                UserId = CurrentUserId(),
                ProductName = productInputs.ProductName,
                ProductNotes = productInputs.ProductNotes
            };

            var result = mediator.Send(command);

            return RedirectToAction("View", new { productId = productInputs.ProductId });
        }

        [HttpPost]
        [Route("{productId}/addtag")]
        public virtual ActionResult AddTag(int productId, string tagName)
        {
            var command = new AddTagToProductCommand
            {
                ProductId = productId,
                TagName = tagName,
                UserId = CurrentUserId()
            };

            var result = mediator.Send(command);

            return RedirectToAction("View", new { productId = productId });
        }

        [HttpPost]
        [Route("{productId}/removetag")]
        public virtual ActionResult RemoveTag(int productId, int userProductTagId)
        {
            var command = new RemoveTagFromProductCommand
            {
                ProductId = productId,
                UserId = CurrentUserId(),
                UserProductTagId = userProductTagId
            };

            var result = mediator.Send(command);

            return RedirectToAction("View", new { productId = productId });
        }
    }
}