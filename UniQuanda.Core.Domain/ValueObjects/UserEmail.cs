namespace UniQuanda.Core.Domain.ValueObjects
{
    public class UserEmail
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsMain  { get; set; }
    }
}
