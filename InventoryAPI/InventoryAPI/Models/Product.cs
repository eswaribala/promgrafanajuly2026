

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductId")]
        public long ProductId { get; set; }
        [Column("ProductName", TypeName = "varchar(200)")]
        public string? ProductName { get; set; }
        //value object
        public ProductDescription? productDescription { get; set; }
        [Column("SKU", TypeName = "varchar(200)")]
        public string? SKU { get; set; }
        [ForeignKey("Category")]
        [Column("Category_Id_FK")]
        public long CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
