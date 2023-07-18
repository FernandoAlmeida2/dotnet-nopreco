using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_nopreco.Models;

namespace dotnet_nopreco.Dtos.Product
{
    public class ProductReqDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Price { get; set; }
        public CategoryType? Category { get; set; }
    }
}