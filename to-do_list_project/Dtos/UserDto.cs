namespace to_do_list_project.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword { get; set; }=string.Empty ;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicturePath { get; set; } = string.Empty;
    }
}
