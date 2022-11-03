using System;
namespace TODOs.Api.Models.Responses
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Status { get; set; }
        public int? ListId { get; set; }
    }
}

