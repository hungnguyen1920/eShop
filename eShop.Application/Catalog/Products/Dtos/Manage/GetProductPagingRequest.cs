using System.Collections.Generic;
using eShop.Application.CommonDtos;

namespace eShop.Application.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keywork { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}