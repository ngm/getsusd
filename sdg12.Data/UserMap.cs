using FluentNHibernate.Mapping;
using sdg12.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Data
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.UserName);
            Map(x => x.GivenName);
            Map(x => x.Surname);

            HasMany(x => x.Products)
                .KeyColumn("UserId")
                .Cascade.AllDeleteOrphan();
        }
    }
}
