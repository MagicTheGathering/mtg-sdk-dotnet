// <copyright file="CardService.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Dto;
    using Model;
    using MtgApiManager.Lib.Core;
    using Utility;

    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public class CardService
        : ServiceBase<CardService, Card>, IMtgQueryable<CardService, CardQueryParameter>
    {
        /// <summary>
        /// The list of queries to apply.
        /// </summary>
        private NameValueCollection _whereQueries = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        public CardService(IMtgApiServiceAdapter serviceAdapter)
            : this(serviceAdapter, ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class. Defaults to version 1.0 of the API.
        /// </summary>
        public CardService()
            : this(new MtgApiServiceAdapter(), ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the MTG API.</param>
        /// <param name="version">The version of the API</param>
        public CardService(IMtgApiServiceAdapter serviceAdapter, ApiVersion version)
            : base(serviceAdapter, version, ApiEndPoint.Cards)
        {
            this._whereQueries = new NameValueCollection();
        }

        /// <summary>
        /// Maps a collection of card DTO objects to the card model.
        /// </summary>
        /// <param name="cardListDto">The list of cards DTO objects.</param>
        /// <returns>A list of card models.</returns>
        public static List<Card> MapCardsList(RootCardListDto cardListDto)
        {
            if (cardListDto == null)
            {
                throw new ArgumentNullException("cardListDto");
            }

            if (cardListDto.Cards == null)
            {
                return null;
            }

            return cardListDto.Cards
                .Select(x => new Card(x))
                .ToList();
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing all the items.</returns>
        public override Exceptional<List<Card>> All()
        {
            try
            {
                var query = this.BuildUri(this._whereQueries);
                var rootCardList = this.CallWebServiceGet<RootCardListDto>(query).Result;

                return Exceptional<List<Card>>.Success(CardService.MapCardsList(rootCardList));
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<Card>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing all the items.</returns>
        public async override Task<Exceptional<List<Card>>> AllAsync()
        {
            try
            {
                var query = this.BuildUri(this._whereQueries);
                var rootCardList = await this.CallWebServiceGet<RootCardListDto>(query).ConfigureAwait(false);

                return Exceptional<List<Card>>.Success(CardService.MapCardsList(rootCardList));
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
        public Exceptional<Card> Find(int multiverseId)
        {
            try
            {
                var rootCard = this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString())).Result;
                var model = new Card(rootCard.Card);

                return Exceptional<Card>.Success(model);
            }
            catch (AggregateException ex)
            {
                return Exceptional<Card>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public Exceptional<Card> Find(string id)
        {
            try
            {
                var rootCard = this.CallWebServiceGet<RootCardDto>(this.BuildUri(id)).Result;
                var model = new Card(rootCard.Card);

                return Exceptional<Card>.Success(model);
            }
            catch (AggregateException ex)
            {
                return Exceptional<Card>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The multi verse identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public async Task<Exceptional<Card>> FindAsync(int multiverseId)
        {
            try
            {
                var rootCard = await this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString())).ConfigureAwait(false);
                var model = new Card(rootCard.Card);

                return Exceptional<Card>.Success(model);
            }
            catch (Exception ex)
            {
                return Exceptional<Card>.Failure(ex);
            }
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="id">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public async Task<Exceptional<Card>> FindAsync(string id)
        {
            try
            {
                var rootCard = await this.CallWebServiceGet<RootCardDto>(this.BuildUri(id)).ConfigureAwait(false);
                var model = new Card(rootCard.Card);

                return Exceptional<Card>.Success(model);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardSubTypes.GetDescription()));
                var rootTypeList = this.CallWebServiceGet<RootCardSubTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.SubTypes);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardSubTypes.GetDescription()));
                var rootTypeList = await this.CallWebServiceGet<RootCardSubTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SubTypes);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardSuperTypes.GetDescription()));
                var rootTypeList = this.CallWebServiceGet<RootCardSuperTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.SuperTypes);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardSuperTypes.GetDescription()));
                var rootTypeList = await this.CallWebServiceGet<RootCardSuperTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SuperTypes);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardTypes.GetDescription()));
                var rootTypeList = this.CallWebServiceGet<RootCardTypeDto>(url).Result;

                return Exceptional<List<string>>.Success(rootTypeList.Types);
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
                var url = new Uri(new Uri(BaseMtgUrl), string.Concat(this.Version.GetDescription(), "/", ApiEndPoint.CardTypes.GetDescription()));
                var rootTypeList = await this.CallWebServiceGet<RootCardTypeDto>(url).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.Types);
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
                throw new ArgumentNullException("property");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            MemberExpression expression = property.Body as MemberExpression;
            var queryName = QueryUtility.GetQueryPropertyName<CardQueryParameter>(expression.Member.Name);

            Type valueType = value.GetType();
            if (valueType.IsArray)
            {
                string val = string.Join("|", (IEnumerable<object>)value);
                this._whereQueries[queryName] = val;
            }
            else
            {
                this._whereQueries[queryName] = Convert.ToString(value);
            }

            return this;
        }
    }
}