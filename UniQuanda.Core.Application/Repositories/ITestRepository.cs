using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface ITestRepository
    {
				/// <summary>
        ///     Gets random automatic test questions from given tags
        /// </summary>
        /// <param name="tagsIds">Tags ids from which random questions will be selected</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>IEnumerable of parent tags</returns>
				Task<IEnumerable<AutomaticTestQuestionEntity>> GetAutomaticTestQuestionsAsync(IEnumerable<int> tagsIds, CancellationToken ct);
		}
}