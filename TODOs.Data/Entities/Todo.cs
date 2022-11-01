using System;
namespace TODOs.Data.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Status { get; set; }
        public int ListId { get; set; }
        public TodoList List { get; set; }
    }
}

