using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace KTTH_BE.Models.ViewModel
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }

        [Display(Name = "Số lượng")]
        public int quantity { get; set; } = 1;

        public decimal estimatedValue { get; set; }
        public int PageSize { get; set; } = 3;
        public List<Product> RelatedProducts { get; set; }
        public IPagedList<Product> TopProducts { get; set; }
    }
}