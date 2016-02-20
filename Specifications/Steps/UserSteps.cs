using sdg12.Core;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace Specifications.Steps
{
    [Binding]
    public class UserSteps
    {
        [Given(@"(.*) is an ethical consumer")]
        public void GivenUserExists(string userName)
        {
            var user = new User
            {
                Name = userName
            };
            ScenarioContext.Current.Set(user);
        }
    }
}
