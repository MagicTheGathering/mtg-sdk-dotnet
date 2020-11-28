using System.Collections.Generic;
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
        /// <returns>A <see cref="Exceptional{T}"/> representing the result containing all the items.</returns>
        Task<Exceptional<List<Set>>> AllAsync();

        /// <summary>
        /// Find a specific card by its set code.
        /// </summary>
        /// <param name="code">The set code to query for.</param>
        /// <returns>A <see cref="Exceptional{Set}"/> representing the result containing a <see cref="Set"/> or an exception.</returns>
        Task<Exceptional<Set>> FindAsync(string code);

        /// <summary>
        ///  Generates a booster pack for a specific set asynchronously.
        /// </summary>
        /// <param name="code">The set code to generate a booster for.</param>
        /// <returns>A <see cref="Exceptional{T}"/> representing the result containing a <see cref="List{Card}"/> or an exception.</returns>
        Task<Exceptional<List<Card>>> GenerateBoosterAsync(string code);
    }
}