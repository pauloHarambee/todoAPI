using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todoitems.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100, MinimumLength = 5)]
        public required string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
