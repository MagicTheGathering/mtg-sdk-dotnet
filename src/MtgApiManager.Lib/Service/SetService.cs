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
    internal class SetService : ServiceBase<Set>, ISetService
    {
        public SetService(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            ApiVersion version,
            bool rateLimitOn = true)
            : base(serviceAdapter, modelMapper, version, ApiEndPoint.Sets, rateLimitOn)
        {
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public ISetService Where<U>(Expression<Func<SetQueryParameter, U>> property, U value)
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