using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace to_do_list_project.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<ToDo> ToDos { get; set; } = new List<ToDo>();
    }
}
