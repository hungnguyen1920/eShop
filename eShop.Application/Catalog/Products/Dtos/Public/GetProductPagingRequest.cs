using System;
using eShop.Application.CommonDtos;

namespace eShop.Application.Catalog.Products.Dtos.Public
{
	public class GetProductPagingRequest : PagingRequestBase
	{
		public int? CategoryId { get; set; }
	}
}