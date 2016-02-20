using FluentNHibernate.Mapping;
using sdg12.Core;
using sdg12.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Data
{
    public class UserProductTagMap : ClassMap<UserProductTag>
    {
        public UserProductTagMap()
        {
            Id(x => x.Id);

            References(x => x.UserProduct).Column("UserProductId");
            References(x => x.Tag).Column("TagId");
        }
    }
}
