using WebStore.Entities.Entities;

namespace WebStore.Entities.ViewModels.Admin
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
