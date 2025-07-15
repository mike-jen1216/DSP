using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSP.Models.Entity
{
    [Table("design_patterns")]
    public class DesignPatternEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string? Summary { get; set; }

        public string? SnipCodeCSharp { get; set; }

        public string? VisualAssetUrl { get; set; }

        public DateTime CreateTime { get; set; }

        public string? ModifiedByUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}