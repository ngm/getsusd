using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Core.Entities
{
    public class UserProductTag
    {
        public virtual int Id { get; set; }
        public virtual UserProduct UserProduct { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
