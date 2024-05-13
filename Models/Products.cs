using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingApplication.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        public int ProductCategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Ratings")]
        public decimal ProductRatings { get; set;}

        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}
