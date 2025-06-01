using Microsoft.AspNetCore.Components.QuickGrid;
using Moamalat.Entities;
using Moamalat.Services;



namespace Moamalat.Components
{
    public class EntityListComponentBase<TItem> : LocalizationComponent
    {
        public IServiceBase<TItem> _Service { get; set; }

        protected GridItemsProvider<TItem>? DoFetchEntityPagedData;
        protected void SetService(IServiceBase<TItem> serviceBase)
        {
            _Service = serviceBase;
        }
        public PaginationState _PaginationState { get; set; } = new PaginationState();
        public PaginationResult<TItem> Pagination { get; set; } = new PaginationResult<TItem>()
        {
            PageSize = 10,
            PageNumber = 1,
            Items = new List<TItem>()
        };
        protected override async Task OnInitializedAsync()
        {
            DoFetchEntityPagedData = async Req =>
            {
                await DoFetch();
                return GridItemsProviderResult.From<TItem>(items: Pagination.Items.ToList(),
                                                    totalItemCount: Pagination.RecordsCount);
            };

            //return base.OnInitializedAsync();
        }
        public async Task DoFetch()
        {
            if (Pagination == null) Pagination = new PaginationResult<TItem>()
            {
                PageSize = 10,
                PageNumber = 1,
                Items = new List<TItem>()
            };
            Pagination.PageNumber = Pagination.PageNumber;
            Pagination.PageSize = Pagination.PageSize;
            Pagination.PageNumber = _PaginationState.CurrentPageIndex + 1;
            Pagination = await _Service.GetPaged(Pagination);

            if (Pagination?.Items == null)
                Pagination.Items = Enumerable.Empty<TItem>();

            StateHasChanged();
        }
        protected async Task RefreshAsync()
        {
            // Reset to first page
            await _PaginationState.SetCurrentPageIndexAsync(0);

            // Force grid to refresh by calling DoFetch
            await DoFetch();
        }
        protected async Task RefreshCurrentPageAsync()
        {
            await DoFetch();
        }
    }
}
