using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingApplication.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Category Order")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
