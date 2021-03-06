﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Core.Entities
{
    public class UserProduct
    {
        public UserProduct()
        {
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Notes { get; set; }
        public virtual IList<UserProductTag> Tags { get; set; }
    }
}
