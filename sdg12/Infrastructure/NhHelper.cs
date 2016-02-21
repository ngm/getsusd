using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.Mapping;
using sdg12.Data;
using NHibernate.Tool.hbm2ddl;

namespace sdg12.Infrastructure
{
    public static class NhHelper
    {
        // http://stackoverflow.com/q/6204511/206297
        public static FluentMappingsContainer AddFromAssemblyOf<T>(this FluentMappingsContainer mappings, Predicate<Type> where)
        {
            if (where == null)
                return mappings.AddFromAssemblyOf<T>();

            var mappingClasses = typeof(T).Assembly.GetExportedTypes()
                .Where(x => IsMappingOf<IMappingProvider>(x) ||
                            IsMappingOf<IIndeterminateSubclassMappingProvider>(x) ||
                            IsMappingOf<IExternalComponentMappingProvider>(x) ||
                            IsMappingOf<IFilterDefinition>(x))
                .Where(x => where(x));

            foreach (var type in mappingClasses)
            {
                mappings.Add(type);
            }

            return mappings;
        }

        private static bool IsMappingOf<T>(Type type)
        {
            return !type.IsGenericType && typeof(T).IsAssignableFrom(type);
        }


        public static ISessionFactory MsSqlSessionFactory
        {
            get
            {
                return Fluently.Configure()
                    .Database(MonoSQLiteConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("sqliteInMemory")))
                    //.Database(SQLiteConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("sqliteInMemory")))
                    //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("TestData")))
                    .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<UserMap>())
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
                    .BuildSessionFactory();
            }
        }
    }
}
