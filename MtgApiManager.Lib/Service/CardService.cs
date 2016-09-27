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
    using Core.Exceptions;
    using Dto.Cards;
    using Model.Card;
    using MtgApiManager.Lib.Core;
    using Utility;

    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public class CardService
        : ServiceBase<CardService, Card>, IMtgQueryable<CardService, CardDto>
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
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Exceptional{List{Card}}"/> representing the result containing all the items.</returns>
        public override Exceptional<List<Card>> All()
        {
            try
            {
                var query = this.BuildUri(this._whereQueries);
                var rootCardList = this.CallWebServiceGet<RootCardListDto>(query).Result;

                return Exceptional<List<Card>>.Success(this.MapCardsList(rootCardList));
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
                var rootCardList = await this.CallWebServiceGet<RootCardListDto>(query);

                return Exceptional<List<Card>>.Success(this.MapCardsList(rootCardList));
            }
            catch (AggregateException ex)
            {
                return Exceptional<List<Card>>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public Exceptional<Card> Find(int multiverseId)
        {
            try
            {
                var rootCard = this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString())).Result;
                var model = new Card();
                model.MapCard(rootCard.Card);

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
        /// <param name="multiverseId">The identifier to query for.</param>
        /// <returns>A <see cref="Exceptional{Card}"/> representing the result containing a <see cref="Card"/> or an exception.</returns>
        public async Task<Exceptional<Card>> FindAsync(int multiverseId)
        {
            try
            {
                var rootCard = await this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString()));
                var model = new Card();
                model.MapCard(rootCard.Card);

                return Exceptional<Card>.Success(model);
            }
            catch (AggregateException ex)
            {
                return Exceptional<Card>.Failure(ex.Flatten().InnerException);
            }
        }

        /// <summary>
        /// Adds a query parameter.
        /// </summary>
        /// <typeparam name="U">The type of property to add the query for.</typeparam>
        /// <param name="property">The property to add the query for.</param>
        /// <param name="value">The value of the query.</param>
        /// <returns>The instance of its self with the new query parameter.</returns>
        public CardService Where<U>(Expression<Func<CardDto, U>> property, string value)
        {
            MemberExpression expression = property.Body as MemberExpression;
            var queryName = QueryUtility.GetQueryPropertyName<CardDto>(expression.Member.Name);

            if (!string.IsNullOrWhiteSpace(queryName))
            {
                this._whereQueries[queryName] = value;
            }

            return this;
        }

        /// <summary>
        /// Maps a collection of card DTO objects to the card model.
        /// </summary>
        /// <param name="cardListDto">The list of cards DTO objects.</param>
        /// <returns>A list of card models.</returns>
        private List<Card> MapCardsList(RootCardListDto cardListDto)
        {
            if (cardListDto == null)
            {
                throw new ArgumentException("cardListDto");
            }

            if (cardListDto.Cards == null)
            {
                return null;
            }

            return cardListDto.Cards
                .Select(x =>
                {
                    var newCard = new Card();
                    newCard.MapCard(x);
                    return newCard;
                })
                .ToList();
        }
    }
}