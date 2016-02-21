using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Core.Entities
{
    public class User
    {
        public User()
        {
            Products = new List<UserProduct>();
        }

        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<UserProduct> Products { get; set; }
        public virtual string GivenName { get; set; }
        public virtual string Surname { get; set; }
    }
}
