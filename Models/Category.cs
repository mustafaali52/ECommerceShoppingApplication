using System.ComponentModel.DataAnnotations;

namespace ECommerceShoppingApplication.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
