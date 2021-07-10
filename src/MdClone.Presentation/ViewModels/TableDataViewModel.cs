using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class TableDataViewModel : ObjectViewModel<ITableDataModel>
    {
        public TableDataViewModel(ITableDataModel model)
            : base(model)
        {

        }
    }
}