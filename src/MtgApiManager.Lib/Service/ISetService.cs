using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Object representing a MTG set.
    /// </summary>
    public interface ISetService : IMtgQueryable<ISetService, SetQueryParameter>
    {
        /// <summary>
        /// Gets all the <see cref="Set"/> defined by the query parameters.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>An object representing the result containing all the items.</returns>
        Task<IOperationResult<List<ISet>>> AllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a specific card by its set code.
        /// </summary>
        /// <param name="code">The set code to query for.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>An object representing the result containing a <see cref="Set"/> or an exception.</returns>
        Task<IOperationResult<ISet>> FindAsync(string code, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates a booster pack for a specific set asynchronously.
        /// </summary>
        /// <param name="code">The set code to generate a booster for.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>An object representing the result containing a <see cref="List{Card}"/> or an exception.</returns>
        Task<IOperationResult<List<ICard>>> GenerateBoosterAsync(string code, CancellationToken cancellationToken = default);
    }
}