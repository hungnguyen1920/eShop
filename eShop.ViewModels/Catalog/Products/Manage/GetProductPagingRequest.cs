using eShop.ViewModels.Common;

namespace eShop.ViewModels.Catalog.Products.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keywork { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}