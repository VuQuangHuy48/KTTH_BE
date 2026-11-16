using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace KTTH_BE.Models.ViewModel
{
    public class ProductSearchVM
    {
        [Display(Name = "Từ khóa tìm kiếm")]
        public string SearchTerm { get; set; }

        [Display(Name = "Giá tối thiểu")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Giá tối đa")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Sắp xếp theo")]
        public string SortOrder { get; set; }

        public IPagedList<Product> Products { get; set; }
    }
}