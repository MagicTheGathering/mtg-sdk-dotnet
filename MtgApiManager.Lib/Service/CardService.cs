// <copyright file="Card.cs" company="Team7 Productions">
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
    using Dto.Cards;
    using Model.Card;
    using MtgApiManager.Lib.Core;
    using Utility;

    /// <summary>
    /// Object representing a mtg card.
    /// </summary>
    public class CardService
        : ServiceBase<CardService, Card>, IMtgQueryable<CardService, CardDto>
    {
        /// <summary>
        /// The list of queries to apply.
        /// </summary>
        private NameValueCollection _whereQueries = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class. Defaults to version 1.0 of the api.
        /// </summary>
        /// <param name="serviceAdapter">The service adapter used to interact with the mtg api.</param>
        public CardService(IMtgApiServiceAdapter serviceAdapter)
            : this(serviceAdapter, ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class. Defaults to version 1.0 of the api.
        /// </summary>
        public CardService()
            : this(new MtgApiServiceAdapter(), ApiVersion.V1_0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="serviceAdapter"></param>
        /// <param name="version"></param>
        public CardService(IMtgApiServiceAdapter serviceAdapter, ApiVersion version)
            : base(serviceAdapter, version, ApiEndPoint.Cards)
        {
            this._whereQueries = new NameValueCollection();
        }

        /// <summary>
        /// Gets all the <see cref="TModel"/> defined by the query parameters.
        /// </summary>
        /// <returns>A <see cref="Card"/> representing all the items.</returns>
        public override List<Card> All()
        {
            // TODO: Add result return.
            var query = this.BuildUri(this._whereQueries);
            var rootCardList = this.CallWebServiceGet<RootCardListDto>(query).Result;
            return this.MapCardsList(rootCardList);
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The identifier to query for.</param>
        /// <returns>A <see cref="CardService"/> belonging to the specified identifier.</returns>
        public Card Find(int multiverseId)
        {
            var rootCard = this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString())).Result;
            var model = new Card();
            model.MapCard(rootCard.Card);

            return model;
        }

        /// <summary>
        /// Find a specific card by its multi verse identifier.
        /// </summary>
        /// <param name="multiverseId">The identifier to query for.</param>
        /// <returns>A <see cref="CardService"/> belonging to the specified identifier.</returns>
        public async Task<Card> FindAsync(int multiverseId)
        {
            var rootCard = await this.CallWebServiceGet<RootCardDto>(this.BuildUri(multiverseId.ToString()));
            var model = new Card();
            model.MapCard(rootCard.Card);

            return model;
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
                _whereQueries[queryName] = value;
            }

            return this;
        }

        /// <summary>
        /// Maps a collection of card dto objects to the card model.
        /// </summary>
        /// <param name="cardListDto">The list of cards dto objects.</param>
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