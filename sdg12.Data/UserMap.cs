using FluentNHibernate.Mapping;
using sdg12.Core.Entities;

namespace sdg12.Data
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.GivenName);
            Map(x => x.Surname);

            HasMany(x => x.Products)
                .KeyColumn("UserId")
                .Cascade.AllDeleteOrphan();
        }
    }
}