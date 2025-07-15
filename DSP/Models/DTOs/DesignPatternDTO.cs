using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSP.Models.DTOs
{
    public class DesignPatternDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string? Summary { get; set; }

        public string? SnipCodeCSharp { get; set; }

        public string? VisualAssetUrl { get; set; }
    }
}