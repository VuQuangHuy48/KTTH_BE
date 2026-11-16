using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace KTTH_BE.Models.ViewModel
{
    public class HomeProductVM
    {
        [Display(Name = "Tìm kiếm sản phẩm")]
        public string SearchTerm { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public IPagedList<Product> NewProducts { get; set; }
    }
}