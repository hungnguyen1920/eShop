using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.ViewModels.Catalog.Products;
using eShop.ViewModels.Common;

namespace eShop.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();
    }
}