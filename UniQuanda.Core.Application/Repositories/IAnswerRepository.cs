using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IAnswerRepository
    {
        /// <summary>
        ///     Gets answers on user profile using paging
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="take">How many results to take</param>
        /// <param name="skip">How many first results to skip</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct);

        /// <summary>
        ///     Get count of answers of user 
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct);
    }
}
