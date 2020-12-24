using System.Collections.Generic;
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
        /// <returns>A <see cref="IOperationResult{T}"/> representing the result containing all the items.</returns>
        Task<IOperationResult<List<ICard>>> AllAsync();

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The multi verse identifier to query for.</param>
        /// <returns>A <see cref="IOperationResult{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        Task<IOperationResult<ICard>> FindAsync(int multiverseId);

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <returns>A <see cref="IOperationResult{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        Task<IOperationResult<ICard>> FindAsync(string id);

        /// <summary>
        /// Gets a list of all the card sub types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        Task<IOperationResult<List<string>>> GetCardSubTypesAsync();

        /// <summary>
        /// Gets a list of all the card super types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        Task<IOperationResult<List<string>>> GetCardSuperTypesAsync();

        /// <summary>
        /// Gets a list of all the card types.
        /// </summary>
        /// <returns>A list of all the card types.</returns>
        Task<IOperationResult<List<string>>> GetCardTypesAsync();

        /// <summary>
        /// Gets a list of all game formats.
        /// </summary>
        /// <returns>A list of all game formats.</returns>
        Task<IOperationResult<List<string>>> GetFormatsAsync();
    }
}