using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.CommonDtos;
using eShop.Data.EF;
using eShop.Data.Entities;

namespace eShop.Application.Catalog.Products
{
    public class ManageProductService : IProductServices
    {
        private readonly eShopDbContext _context;

        public ManageProductService(eShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Delete(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(ProductEditRequest request)
        {
            throw new System.NotImplementedException();
        }

        Task<List<ProductViewModel>> IProductServices.GetAll()
        {
            throw new System.NotImplementedException();
        }

        Task<PagedViewModel<ProductViewModel>> IProductServices.GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}