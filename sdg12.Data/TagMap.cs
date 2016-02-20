using FluentNHibernate.Mapping;
using sdg12.Core.Entities;

namespace sdg12.Data
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
        }
    }
}