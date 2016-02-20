using sdg12.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdg12.Service.Messages
{
    public class GetProductsQuery
    {
        public int UserId { get; set; }
    }

    public class GetProductsResponse
    {
        public IList<UserProductDto> Products { get; set; }
    }
}
