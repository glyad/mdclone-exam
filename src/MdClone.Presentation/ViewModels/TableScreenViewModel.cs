using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class TableScreenViewModel : ObjectViewModel<ITableDataModel>
    {
        public TableScreenViewModel(ITableDataModel model)
            : base(model)
        {

        }
    }
}