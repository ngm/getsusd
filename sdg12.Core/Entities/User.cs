using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Core
{
    public class User
    {
        public User()
        {
            Products = new List<UserProduct>();
        }

        public string Name { get; set; }
        public IList<UserProduct> Products { get; set; }
    }
}
