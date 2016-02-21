using FluentAssertions;
using NHibernate;
using NHibernate.Linq;
using sdg12.Core.Entities;
using sdg12.Service.Handlers;
using sdg12.Service.Messages;
using System.Linq;
using TechTalk.SpecFlow;

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

        [Given(@"(.*) has saved the following products")]
        public void GivenUserHasSavedTheFollowingProducts(string userName, Table table)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var user = session.Query<User>().First(u => u.UserName == userName);

                foreach (var row in table.Rows)
                {
                    var command = new AddProductCommand
                    {
                        UserId = user.Id,
                        ProductName = row["Name"],
                        ProductNotes = row["Notes"]
                    };

                    var handler = new AddProductCommandHandler(session);
                    var response = handler.Handle(command);

                    if (row.ContainsKey("Tags") && !string.IsNullOrWhiteSpace(row["Tags"]))
                    {
                        var addTagHandler = new AddTagToProductCommandHandler(session);
                        var tagNames = row["Tags"].Split(',').Select(t => t.Trim());
                        foreach (var tagName in tagNames)
                        {
                            var addTagCommand = new AddTagToProductCommand
                            {
                                UserId = user.Id,
                                ProductId = response.NewProductId,
                                TagName = tagName
                            };
                            addTagHandler.Handle(addTagCommand);
                        }
                    }
                }
            }
        }

        [When(@"(.*) views their list of products")]
        public void WhenBartViewsHisListOfProducts(string userName)
        {
            var user = sessionFactory.OpenSession().Query<User>().First(u => u.UserName == userName);

            var query = new GetProductsQuery
            {
                UserId = user.Id
            };
            var handler = new GetProductsQueryHandler(sessionFactory.OpenSession());
            var response = handler.Handle(query);
            ScenarioContext.Current.Set(response);
        }

        [Then(@"(.*)'s list of products should be as follows:")]
        public void ThenTheAllOfHisProductShouldBeVisible(string userName, Table table)
        {
            var response = ScenarioContext.Current.Get<GetProductsResponse>();
            foreach (var row in table.Rows)
            {
                var productName = row["Name"];
                var productNotes = row["Notes"];
                response.Products.Any(p => p.ProductName == productName && p.ProductNotes == productNotes)
                    .Should().BeTrue();
            }
        }

        [When(@"(.*) changes the name of '(.*)' to '(.*)'")]
        public void WhenBartChangesTheNameOfTo(string userName, string originalName, string newName)
        {
            var user = sessionFactory.OpenSession().Query<User>().First(u => u.UserName == userName);
            var product = sessionFactory.OpenSession().Query<UserProduct>().First(p => p.Name == originalName);

            var command = new EditProductCommand
            {
                ProductId = product.Id,
                UserId = user.Id,
                ProductName = newName,
                ProductNotes = product.Notes
            };

            var handler = new EditProductCommandHandler(sessionFactory.OpenSession());
            handler.Handle(command);
        }

        [Then(@"the following products exist")]
        public void ThenTheFollowingProductsExist(Table table)
        {
            using (var session = sessionFactory.OpenSession())
            {
                foreach (var row in table.Rows)
                {
                    var productName = row["Name"];
                    session.Query<UserProduct>().First(p => p.Name == productName);
                }
            }
        }

        [When(@"(.*) tags '(.*)' with '(.*)'")]
        public void WhenBartTagsWith(string userName, string productName, string tagName)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var user = session.Query<User>().First(u => u.UserName == userName);
                var product = session.Query<UserProduct>().First(p => p.Name == productName);

                var tagUserProductCommand = new AddTagToProductCommand()
                {
                    ProductId = product.Id,
                    UserId = user.Id,
                    TagName = tagName
                };

                var result = new AddTagToProductCommandHandler(session).Handle(tagUserProductCommand);
            }
        }

        [Then(@"the '(.*)' product should have the '(.*)' tag")]
        public void ThenTheProductShouldHaveTheTag(string productName, string tagName)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var userProduct = session.Query<UserProduct>().First(p => p.Name == productName);
                userProduct.Tags.Any(t => t.Tag.Name == tagName).Should().BeTrue();
            }
        }

        [When(@"(.*) removes the tag '(.*)' from the '(.*)' product")]
        public void WhenBartRemovesTheTagFromTheProduct(string userName, string tagName, string productName)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var userProductTag = session.Query<UserProductTag>().First(t => t.Tag.Name == tagName);
                var user = session.Query<User>().First(u => u.UserName == userName);
                var product = session.Query<UserProduct>().First(p => p.Name == productName);

                var command = new RemoveTagFromProductCommand()
                {
                    ProductId = product.Id,
                    UserId = user.Id,
                    UserProductTagId = userProductTag.Id
                };

                var result = new RemoveTagFromProductCommandHandler(session).Handle(command);
            }
        }

        [Then(@"the '(.*)' product should not have the '(.*)' tag")]
        public void ThenTheProductShouldNotHaveTheTag(string productName, string tagName)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var product = session.Query<UserProduct>().First(p => p.Name == productName);
                product.Tags.Any(t => t.Tag.Name == tagName).Should().BeFalse();
            }
        }
    }
}