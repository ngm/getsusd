using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using sdg12.Data;
using TechTalk.SpecFlow;

namespace Specifications.Steps
{
    public class SessionFactoryHolder
    {
        private static ISessionFactory sessionFactory;

        public static void SetupNhibernateSessionFactory()
        {
            sessionFactory = Fluently.Configure()
                 .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("TestData")))
                 .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<UserMap>())
                 .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))

            .BuildSessionFactory();
        }

        public ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
        }
    }

    [Binding]
    public class DataHooks
    {
        [BeforeTestRun]
        public static void SetupNhibernateSessionFactory()
        {
            SessionFactoryHolder.SetupNhibernateSessionFactory();
        }
    }
}