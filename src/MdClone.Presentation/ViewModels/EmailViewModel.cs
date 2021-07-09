using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailViewModel : EditableObjectViewModel<IEmailModel>
    {
        public EmailViewModel(IEmailModel model) : base(model)
        {
        }

        protected override Task<bool> SaveMethod(IEmailModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}