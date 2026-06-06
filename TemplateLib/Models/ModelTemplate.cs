using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLib.Models
{
    public class ModelTemplate
    {
        public int Id { get; set; }

        public string? Property1 { get; set; } 

        public string? Property2 { get; set; } 

        public int NumberProperty { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Property1: {Property1}, Property2: {Property2}, Number: {NumberProperty}";
        }
    }
}
