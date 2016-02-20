using FluentNHibernate.Mapping;
using sdg12.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Data
{
    public class UserProductMap : ClassMap<UserProduct>
    {
        public UserProductMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Notes);
        }
    }
}
