namespace UniQuanda.Core.Application.CQRS.Commands.Test.GetUserTests
{
    public class GetUserTestsResponseDTO
    {
        public IEnumerable<TestResponseDTO> Tests { get; set; }

        public class TestResponseDTO
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsFinished { get; set; }
            public IEnumerable<TestTagResponseDTO> Tags { get; set; }
        }

        public class TestTagResponseDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
