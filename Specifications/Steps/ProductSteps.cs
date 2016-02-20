using NHibernate.Linq;
using FluentAssertions;
using sdg12.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NHibernate;
using sdg12.Service.Messages;
using sdg12.Service.Handlers;

namespace Specifications.Steps
{
    [Binding]
    public class AddProductSteps
    {
        private readonly ISessionFactory sessionFactory;

        public AddProductSteps(SessionFactoryHolder sessionFactoryHolder)
        {
            this.sessionFactory = sessionFactoryHolder.SessionFactory;
        }


        [When(@"(.*) adds the product:")]
        public void WhenUserAddsTheProduct(string userName, Table table)
        {
            var user = sessionFactory.OpenSession().Query<User>().First(u => u.UserName == userName);

            var command = new AddProductCommand();
            command.UserId = user.Id;
            command.ProductName = table.Rows[0]["Name"];
            command.ProductNotes = table.Rows[0]["Notes"];

            var handler = new AddProductCommandHandler(sessionFactory.OpenSession());
            var result = handler.Handle(command);

            ScenarioContext.Current.Set(command);
        }
        
        [Then(@"the product is added to (.*)'s list of products")]
        public void ThenTheProductIsAddedToBartSListOfProducts(string userName)
        {
            var command = ScenarioContext.Current.Get<AddProductCommand>();
            var user = sessionFactory.OpenSession().Query<User>().First(u => u.UserName == userName);

            var product = user.Products.First();
            product.Name.Should().Be(command.ProductName);
            product.Notes.Should().Be(command.ProductNotes);
        }
    }
}
