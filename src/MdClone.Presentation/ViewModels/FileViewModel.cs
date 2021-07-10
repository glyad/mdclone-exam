using System.IO;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class FileViewModel : ObjectViewModel<IFileModel>
    {
        public FileViewModel(IFileModel model)
            : base(model)
        {

        }

        public override string DisplayName
        {
            get => Path.GetFileName(Model.Name);
            set {  }
        }
    }
}