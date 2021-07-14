using LogoFX.Client.Mvvm.Model;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class EmailRecipientsModel : EditableModel, IEmailRecipientsModel
    {
        private IEmailRecipientModel[] _items;

        public EmailRecipientsModel()
        {
            _items = new IEmailRecipientModel[0];
        }

        public IEmailRecipientModel[] Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
    }
}