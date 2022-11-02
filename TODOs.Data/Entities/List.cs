using System;
using System.Collections.Generic;

namespace TODOs.Data.Entities
{
    public class List
    {
        public int Id { get; set; }
        public string Label { get; set; }

        public virtual List<Todo> Todos { get; set; } = new List<Todo>();
    }
}

