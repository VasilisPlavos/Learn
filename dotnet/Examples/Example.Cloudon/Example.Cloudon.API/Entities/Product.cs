using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Cloudon.API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Code { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Discount { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? RetailPrice { get; set; }
        public string Source { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? WholesalePrice { get; set; }
    }
}