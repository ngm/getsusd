using System.Linq;
using NHibernate;
using sdg12.Core;
using sdg12.Service.Messages;
using System;
using sdg12.Service.Dtos;
using MediatR;

namespace sdg12.Service.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly ISession nhSession;

        public GetProductsQueryHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public GetProductsResponse Handle(GetProductsQuery message)
        {
            var user = nhSession.Get<User>(message.UserId);

            var products = user.Products
                .Select(p => new UserProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductNotes = p.Notes
                }).ToList();

            return new GetProductsResponse
            {
                Products = products
            };
        }
    }
}