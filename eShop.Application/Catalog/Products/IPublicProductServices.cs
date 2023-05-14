using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.CommonDtos;

namespace eShop.Application.Catalog.Products
{
    public interface IPublicProductServices
    {
        PagedViewModel<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize);
    }
}