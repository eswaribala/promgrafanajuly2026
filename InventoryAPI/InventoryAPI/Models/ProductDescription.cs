using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    [Owned]
    public class ProductDescription
    {
        [Column("PurchasedDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime PurchasedDate { get; set; }
        [Column("ExpiryDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime ExpiryDate { get; set; }
        [Column("Cost")]
        public long Cost { get; set; }
        [Column("BufferLevel")]
        public long BufferLevel { get; set; }

    }
}
