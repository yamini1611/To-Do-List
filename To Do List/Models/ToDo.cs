#nullable disable
using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string Status {  get; set; }
        public string UserEmail { get; set; }
        public string  Taskname { get; set; }
        public string AssignedDate { get; set; }
    }
}
