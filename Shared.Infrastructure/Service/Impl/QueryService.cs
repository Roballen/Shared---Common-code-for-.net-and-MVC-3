using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Domain.Repository;
using Shared.Infrastructure.Dto;

namespace Shared.Infrastructure.Service.Impl
{
    public class QueryService<TDtoSummary,TDtoDetail, TDomain, TSortBy> 
                                where TDomain : class where TDtoDetail : class 
    {
        private readonly IPagedQueryGenerator<TDomain, TSortBy> _pagedQueryGenerator;

        public QueryService(IEntityRepository<TDomain> repository, IPagedQueryGenerator<TDomain, TSortBy> pagedQueryGenerator)
        {
            Repository = repository;
            _pagedQueryGenerator = pagedQueryGenerator;
        }

        protected IEntityRepository<TDomain> Repository { get; private set; }

        #region IQueryService<TDto,TSortBy> Members

        public PagingListResponse<TDtoSummary> List(PagingListRequest<TSortBy> listRequest, Func<List<TDomain>, List<TDtoSummary>> mappingExp)
        {
            var query = Repository.GetAll();
            var list = _pagedQueryGenerator.CreateQueryFor(query, listRequest).ToList();
            var convertedList = mappingExp(list);
            return new PagingListResponse<TDtoSummary>(convertedList, listRequest.Paging);
        }

        public TDtoDetail Find(UidRequest uidFindRequest, Func<TDomain,TDtoDetail> mappingExp )
        {
            var find = Repository.Find(uidFindRequest.Id);
            return find != null ? mappingExp(find) : null;
        }

        #endregion
    }
}