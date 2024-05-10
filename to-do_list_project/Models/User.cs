using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace to_do_list_project.Models
{
    public class User
    {

       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicturePath { get; set; } = string.Empty; 

        public List<ToDo> ToDos { get; set; } = new List<ToDo>();
    }
}
