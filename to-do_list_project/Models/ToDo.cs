using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace to_do_list_project.Models
{
    public class ToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Descriotion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a due date.")]
        public DateTime DueDate { get; set; }

        public string CatigoryId { get; set; }
        public Catigory Catigory { get; set; } = null!;

        public string StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public string UserId { get; set; }
        public User User { get; set; } = null!;

        public bool OverDue => StatusId == "open" && DueDate < DateTime.Today;

    }
}
