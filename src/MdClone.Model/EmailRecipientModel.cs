using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Model;
using MdClone.Model.Contracts;
using MdClone.Model.Validation;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class EmailRecipientModel : EditableModel, IEmailRecipientModel
    {
        private string _address;

        [EmailValidation]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
    }
}