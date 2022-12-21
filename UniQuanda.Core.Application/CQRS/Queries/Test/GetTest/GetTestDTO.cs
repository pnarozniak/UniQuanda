namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetTest
{
    public class GetTestResponseDTO
    {
        public bool IsCreator { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFinished { get; set; }
        public IEnumerable<TestQuestionResponseDTO> Questions { get; set; }
        public IEnumerable<GetTestResponseDTOTag> Tags { get; set; }

        public class GetTestResponseDTOTag
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class TestQuestionResponseDTO
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Header { get; set; }
            public string HTML { get; set; }
            public TestAnswerResponseDTO Answer { get; set; }
        }

        public class TestAnswerResponseDTO
        {
            public int Id { get; set; }
            public string HTML { get; set; }
            public DateTime CreatedAt { get; set; }
            public int CommentsCount { get; set; }
        }
    }
}
