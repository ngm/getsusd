using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Service.Dtos
{
    public class UserProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNotes { get; set; }
        public IList<UserProductTagDto> Tags { get; set; }
    }
}
