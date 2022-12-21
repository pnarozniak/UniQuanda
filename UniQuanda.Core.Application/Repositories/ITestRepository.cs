using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface ITestRepository
    {
				/// <summary>
        ///     Generates random test questions from given tags
        /// </summary>
        /// <param name="idUser">Id of test creator</param>
        /// <param name="tagsIds">Tags ids from which random questions will be selected</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Id of generated test or null if there were not enough questions in given tags</returns>
				Task<int?> GenerateTestAsync(int idUser, IEnumerable<int> tagsIds, CancellationToken ct);

				/// <summary>
        ///     Gets test by id
        /// </summary>
        /// <param name="idTest">Test id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>Test or null if not found</returns>
        Task<TestEntity?> GetTestAsync(int idTest, CancellationToken ct);

        /// <summary>
        ///     Get user tests - only general data (without it's questions)
        /// </summary>
        /// <param name="idUser">User Id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>User tests</returns>
        Task<IEnumerable<TestEntity>> GetUserTestsAsync(int idUser, CancellationToken ct);

        /// <summary>
        ///     Marks user test as finished
        /// </summary>
        /// <param name="idUser">User Id</param>
        /// <param name="idTest">Test Id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns>True if operation was successfull, otherwise False</returns>
        Task<bool> MarkTestAsFinishedAsync(int idUser, int idTest, CancellationToken ct);
		}
}