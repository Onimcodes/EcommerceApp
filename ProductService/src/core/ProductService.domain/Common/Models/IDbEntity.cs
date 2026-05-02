using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.domain.Common.Models
{
    public interface IDbEntity
    {
        public string Id { get; set; }        
    }
}
