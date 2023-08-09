using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Catalog.Products.Dtos.Public;
using eShop.Application.CommonDtos;

namespace eShop.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}