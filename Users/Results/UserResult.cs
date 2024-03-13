namespace Users.Results
{
    public class UserResult
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
    }
}
