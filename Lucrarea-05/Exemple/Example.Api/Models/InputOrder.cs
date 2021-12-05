using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exemple.Domain.Models;

namespace Example.Api.Models
{
    public class InputOrder
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Quantity { get; set; }
    }
}
