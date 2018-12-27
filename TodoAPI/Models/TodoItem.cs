using System;
namespace TodoAPI.Models
{
    public class TodoItem
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsComplete { get; set; }
    }
}
