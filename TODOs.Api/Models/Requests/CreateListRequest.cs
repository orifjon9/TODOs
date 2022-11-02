using System;
using System.ComponentModel.DataAnnotations;

namespace TODOs.Api.Models.Requests
{
    public class CreateListRequest
    {
        [Required]
        [MaxLength(256)]
        public string Label { get; set; }
    }
}

