using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.CommonDtos;

namespace eShop.Application.Catalog.Products
{
    public interface IProductServices
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int productId);
        Task<List<ProductViewModel>> GetAll();
        Task<PagedViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}