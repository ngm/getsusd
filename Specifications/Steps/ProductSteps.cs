using sdg12.Core;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Specifications.Steps
{
    [Binding]
    public class AddProductSteps
    {
        [When(@"(.*) adds the product:")]
        public void WhenUserAddsTheProduct(string userName, Table table)
        {
            var user = ScenarioContext.Current.Get<User>();
            var product = new UserProduct();
            product.Name = table.Rows[0]["Name"];
            product.Notes = table.Rows[0]["Notes"];
            user.Products.Add(product);

            ScenarioContext.Current.Set(product);
        }
        
        [Then(@"the product is added to (.*)'s list of products")]
        public void ThenTheProductIsAddedToBartSListOfProducts(string userName)
        {
            var user = ScenarioContext.Current.Get<User>();
            var product = ScenarioContext.Current.Get<UserProduct>();

            user.Products.Contains(product);
        }
    }
}
