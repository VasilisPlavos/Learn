using System.Collections.Generic;
using System.Linq;
using Example.Cloudon.API.Dtos;
using Example.Cloudon.API.Helpers.Extentions;
using Newtonsoft.Json.Linq;

namespace Example.Cloudon.API.Helpers
{
    public class ProductHelper
    {
        public static List<ProductDto> GetProductsDtos(JToken productsJtoken)
        {
            return productsJtoken.Children()
                .Select(x => new ProductDto()
                {
                    ExternalId = x["externalId"].ToString(),
                    Code = x["code"].ToString(),
                    Description = x["description"].ToString(),
                    Name = x["name"].ToString(),
                    Barcode = x["barcode"].ToString(),
                    RetailPrice = x["retailPrice"].ToString().ConvertToDecimal(),
                    WholesalePrice = x["wholesalePrice"].ToString().ConvertToDecimal(),
                    Discount = x["discount"].ToString().ConvertToDecimal(),
                    Source = "SoftOneProducts"
                })
                .ToList();
        }
    }
}