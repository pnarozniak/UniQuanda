using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities;

namespace UniQuanda.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private IEnumerable<Question> questions =  new List<Question>()
        {
            new ()
            {
                Id = 1,
                Content = "Siemanko",
                Title = "Jakis tytul"
            },
            new ()
            {
                Id = 2,
                Content = "Elo",
                Title = "UniQuanda Works"
            }
        };
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await Task.FromResult(questions);
        }

        public async Task<Question> GetQuestionByIdAndTitleAsync(int id, string title)
        {
            return await Task.FromResult(questions.FirstOrDefault(q => q.Id == id && q.Title == title));
        }

        public async Task<bool> AddQuestionAsync(Question question)
        {
            var newQuestions = new List<Question>(questions);
            newQuestions.Add(question);
            this.questions = newQuestions;
            return await Task.FromResult(true);
        }
    }
}
