﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Models
{
    public class ToDo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public int Id { get; set; }

        public DateTime? DueDate { get; set; }

        public ToDo()
        {
            IsCompleted = false;
            Id = 0;
            DueDate = DateTime.Today;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description}";
        }
    }
}
