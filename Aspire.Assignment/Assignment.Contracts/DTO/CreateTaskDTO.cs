using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Contracts.DTO
{
    public class CreateTaskDTO
    {
        public int TaskId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }

        public string TaskDescription { get; set; }
        public IFormFile File { get; set; }

        public string Status { get; set; }
    }
}
