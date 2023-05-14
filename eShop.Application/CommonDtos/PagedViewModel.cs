using System.Collections.Generic;

namespace eShop.Application.CommonDtos
{
    public class PagedViewModel<T>
    {
        public int TotalRecord { get; set; }
        public List<T> Items { get; set; }
    }
}