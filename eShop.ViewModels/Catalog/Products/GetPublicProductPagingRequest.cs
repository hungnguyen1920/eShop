using System;
using eShop.ViewModels.Common;

namespace eShop.ViewModels.Catalog.Products
{
	public class GetPublicProductPagingRequest : PagingRequestBase
	{
		public int? CategoryId { get; set; }
		public string LanguageId { get; set; }
	}
}