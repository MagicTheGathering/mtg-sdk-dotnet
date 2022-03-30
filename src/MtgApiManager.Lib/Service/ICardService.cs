using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public interface ICardService : IMtgQueryable<ICardService, CardQueryParameter>
    {
        /// <summary>
        /// Gets all the <see cref="Card"/> defined by the query parameters.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="IOperationResult{T}"/> representing the result containing all the items.</returns>
        Task<IOperationResult<List<ICard>>> AllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The multi verse identifier to query for.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="IOperationResult{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        Task<IOperationResult<ICard>> FindAsync(int multiverseId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="IOperationResult{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        Task<IOperationResult<ICard>> FindAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of all the card sub types.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A list of all the card super types.</returns>
        Task<IOperationResult<List<string>>> GetCardSubTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of all the card super types.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A list of all the card super types.</returns>
        Task<IOperationResult<List<string>>> GetCardSuperTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of all the card types.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A list of all the card types.</returns>
        Task<IOperationResult<List<string>>> GetCardTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of all game formats.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A list of all game formats.</returns>
        Task<IOperationResult<List<string>>> GetFormatsAsync(CancellationToken cancellationToken = default);
    }
}