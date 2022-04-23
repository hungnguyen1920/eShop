using eShop.Data.Enums;
using System.Collections.Generic;

namespace eShop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
