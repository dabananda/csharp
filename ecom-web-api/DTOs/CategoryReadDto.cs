using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Ecommerce_Web_API.DTOs
{
    public class CategoryReadDto
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty; // string.Empty defines that description can be null
        public DateTime CategoryCreatedAt { get; set; }
    }
}