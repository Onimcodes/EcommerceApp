using ProductService.domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.domain.Product.Models
{

  
    public class Product : IDbEntity
    {
        public string Id { get; set; }  = Guid.NewGuid().ToString();    
        public string Name { get; set; } = string.Empty;
        //public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        //public bool IsPublished { get; set; } = false;
        //public bool IsFeatured { get; set; } = false;
        //public string? MetaTitle { get; set; }
        //public string? MetaDescription { get; set; }

    }
}
