namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public string Endpoint { get; set; }
        public string? Body { get; set; }
        public string? Client { get; set; }
        public string Headers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
