using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CategoryId",TypeName = "bigint")]
        public long CategoryId { get; set; }
        [Column("CategoryName",TypeName = "varchar(200)")]
        public string? CategoryName { get; set; }
    }
}
