using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Dto.Set;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Object representing a MTG set.
    /// </summary>
    public class SetService
        : ServiceBase<SetService, Set>, IMtgQueryable<SetService, SetQueryParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        public SetService()
            : this(new MtgApiServiceAdapter(), new ModelMapper(), ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetService"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API</param>
        /// <param name="rateLimitOn">Turn the rate limit on or off.</param>
        public SetService(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            ApiVersion version,
            bool rateLimitOn = true)
            : base(serviceAdapter, modelMapper, version, ApiEndPoint.Sets, rateLimitOn)
        {
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Set}}"/> representing the result containing all the items.</returns>
        public async override Task<Exceptional<List<Set>>> AllAsync()
        {
            try
            {
                var query = BuildUri(WhereQueries);
                var rootSetList = await CallWebServiceGet<RootSetListDto>(query).ConfigureAwait(false);
                return Exceptional<List<Set>>.Success(MapSetsList(rootSetList), MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<Set>>.Failure(ex);
            }
        }

        /// <summary>
        /// Find a specific card by its set code.
        /// </summary>
        /// <param name="code">The set code to query for.</param>
        /// <returns>A <see cref="Exceptional{Set}"/> representing the result containing a <see cref="Set"/> or an exception.</returns>
        public async Task<Exceptional<Set>> FindAsync(string code)
        {
            try
            {
                var rootSet = await CallWebServiceGet<RootSetDto>(BuildUri(code)).ConfigureAwait(false);
                var model = ModelMapper.MapSet(rootSet.Set);

                return Exceptional<Set>.Success(model, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<Set>.Failure(ex);
            }
        }

        /// <summary>
        ///  Generates a booster pack for a specific set asynchronously.
        /// </summary>
        /// <param name="code">The set code to generate a booster for.</param>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing a <see cref="List{Card}"/> or an exception.</returns>
        public async Task<Exceptional<List<Card>>> GenerateBoosterAsync(string code)
        {
            try
            {
                var url = new Uri(Path.Combine(BuildUri(code).AbsoluteUri, "booster"));
                var rootCardList = await CallWebServiceGet<RootCardListDto>(url).ConfigureAwait(false);

                var cards = rootCardList.Cards
                .Select(x => ModelMapper.MapCard(x))
                .ToList();

                return Exceptional<List<Card>>.Success(cards, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<Card>>.Failure(ex);
            }
        }

        /// <summary>
        /// Adds a query parameter.
        /// </summary>
        /// <typeparam name="U">The type of property to add the query for.</typeparam>
        /// <param name="property">The property to add the query for.</param>
        /// <param name="value">The value of the query.</param>
        /// <returns>The instance of its self with the new query parameter.</returns>
        public SetService Where<U>(Expression<Func<SetQueryParameter, U>> property, U value)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (EqualityComparer<U>.Default.Equals(value, default))
            {
                throw new ArgumentNullException(nameof(value));
            }

            MemberExpression expression = property.Body as MemberExpression;
            var queryName = QueryUtility.GetQueryPropertyName<SetQueryParameter>(expression.Member.Name);
            WhereQueries[queryName] = Convert.ToString(value);

            return this;
        }

        private List<Set> MapSetsList(RootSetListDto setListDto)
        {
            if (setListDto == null)
            {
                throw new ArgumentNullException(nameof(setListDto));
            }

            if (setListDto.Sets == null)
            {
                return new List<Set>();
            }

            return setListDto.Sets
                .Select(x => ModelMapper.MapSet(x))
                .ToList();
        }
    }
}