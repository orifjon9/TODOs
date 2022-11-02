using System;
using System.ComponentModel.DataAnnotations;

namespace TODOs.Api.Models.Requests
{
    public class CreateUpdateTodoRequest
    {
        [Required]
        [MaxLength(256)]
        public string Label { get; set; }

        public int? ListId { get; set; }
        public bool Status { get; set; }
    }
}

