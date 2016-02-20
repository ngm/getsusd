using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Service.Dtos
{
    public class UserProductTagDto
    {
        public int UserProductTagId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
