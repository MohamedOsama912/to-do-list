namespace to_do_list_project.Dtos
{
    public class ToDoDto
    {
        public string Descriotion { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string CatigoryId { get; set; }
        public string StatusId { get; set; }
    }
}
