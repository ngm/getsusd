using NHibernate;
using sdg12.Core.Entities;
using TechTalk.SpecFlow;

namespace Specifications.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly ISessionFactory sessionFactory;

        public UserSteps(SessionFactoryHolder sessionFactoryHolder)
        {
            this.sessionFactory = sessionFactoryHolder.SessionFactory;
        }

        [Given(@"(.*) is an ethical consumer")]
        public void GivenUserExists(string userName)
        {
            var user = new User
            {
                Id = 1,
                UserName = userName
            };

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Save(user);
                tx.Commit();
            }
        }
    }
}