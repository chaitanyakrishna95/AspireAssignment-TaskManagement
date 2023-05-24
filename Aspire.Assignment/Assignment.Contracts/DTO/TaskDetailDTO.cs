using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Contracts.DTO
{
    public class TaskDetailDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }

        public string TaskDescription { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
    }
}
