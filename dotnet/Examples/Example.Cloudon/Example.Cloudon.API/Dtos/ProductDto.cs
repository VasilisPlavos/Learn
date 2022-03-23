using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Cloudon.API.Dtos
{
    public class ProductDto
    {
        public string Barcode { get; set; }
        public string Code { get; set; }
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
