using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public class CardService
        : ServiceBase<CardService, Card>, IMtgQueryable<CardService, CardQueryParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        public CardService()
            : this(new MtgApiServiceAdapter(), new ModelMapper(), ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API</param>
        /// <param name="rateLimitOn">Turn the rate limit on or off.</param>
        public CardService(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            ApiVersion version,
            bool rateLimitOn = true)
            : base(serviceAdapter, modelMapper, version, ApiEndPoint.Cards, rateLimitOn)
        {
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing all the items.</returns>
        public async override Task<Exceptional<List<Card>>> AllAsync()
        {
            try
            {
                var query = BuildUri(WhereQueries);
                var rootCardList = await CallWebServiceGet<RootCardListDto>(query).ConfigureAwait(false);

                return Exceptional<List<Card>>.Success(MapCardsList(rootCardList), MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<Card>>.Failure(ex);
            }
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The multi verse identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public Exceptional<Card> Find(int multiverseId) => FindAsync(multiverseId.ToString()).Result;

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public Exceptional<Card> Find(string id) => FindAsync(id).Result;

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The multi verse identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public Task<Exceptional<Card>> FindAsync(int multiverseId) => FindAsync(multiverseId.ToString());

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public async Task<Exceptional<Card>> FindAsync(string id)
        {
            try
            {
                var rootCard = await CallWebServiceGet<RootCardDto>(BuildUri(id)).ConfigureAwait(false);
                var model = ModelMapper.MapCard(rootCard.Card);

                return Exceptional<Card>.Success(model, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<Card>.Failure(ex);
            }
        }

        /// <summary>
        /// Gets a list of all the card sub types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        public Exceptional<List<string>> GetCardSubTypes()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardSubTypes.GetDescription()));
                var rootTypeList = CallWebServiceGet<RootCardSubTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.SubTypes, MtgApiController.CreatePagingInfo());
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<string>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Gets a list of all the card sub types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        public async Task<Exceptional<List<string>>> GetCardSubTypesAsync()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardSubTypes.GetDescription()));
                var rootTypeList = await CallWebServiceGet<RootCardSubTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SubTypes, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <summary>
        /// Gets a list of all the card super types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        public Exceptional<List<string>> GetCardSuperTypes()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardSuperTypes.GetDescription()));
                var rootTypeList = CallWebServiceGet<RootCardSuperTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.SuperTypes, MtgApiController.CreatePagingInfo());
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<string>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Gets a list of all the card super types.
        /// </summary>
        /// <returns>A list of all the card super types.</returns>
        public async Task<Exceptional<List<string>>> GetCardSuperTypesAsync()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardSuperTypes.GetDescription()));
                var rootTypeList = await CallWebServiceGet<RootCardSuperTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SuperTypes, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <summary>
        /// Gets a list of all the card types.
        /// </summary>
        /// <returns>A list of all the card types.</returns>
        public Exceptional<List<string>> GetCardTypes()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardTypes.GetDescription()));
                var rootTypeList = CallWebServiceGet<RootCardTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.Types, MtgApiController.CreatePagingInfo());
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<string>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Gets a list of all the card types.
        /// </summary>
        /// <returns>A list of all the card types.</returns>
        public async Task<Exceptional<List<string>>> GetCardTypesAsync()
        {
            try
            {
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(Version.GetDescription(), "/", ApiEndPoint.CardTypes.GetDescription()));
                var rootTypeList = await CallWebServiceGet<RootCardTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.Types, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <summary>
        /// Adds a query parameter.
        /// </summary>
        /// <typeparam name="U">The type of property to add the query for.</typeparam>
        /// <param name="property">The property to add the query for.</param>
        /// <param name="value">The value of the query.</param>
        /// <returns>The instance of its self with the new query parameter.</returns>
        public CardService Where<U>(Expression<Func<CardQueryParameter, U>> property, U value)
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
            var queryName = QueryUtility.GetQueryPropertyName<CardQueryParameter>(expression.Member.Name);

            Type valueType = value.GetType();
            if (valueType.IsArray)
            {
                WhereQueries[queryName] = string.Join("|", (IEnumerable<object>)value);
            }
            else
            {
                WhereQueries[queryName] = Uri.UnescapeDataString(Convert.ToString(value));
            }

            return this;
        }

        private List<Card> MapCardsList(RootCardListDto cardListDto)
        {
            if (cardListDto == null)
            {
                throw new ArgumentNullException(nameof(cardListDto));
            }

            if (cardListDto.Cards == null)
            {
                return new();
            }

            return cardListDto.Cards
                .Select(x => ModelMapper.MapCard(x))
                .ToList();
        }
    }
}