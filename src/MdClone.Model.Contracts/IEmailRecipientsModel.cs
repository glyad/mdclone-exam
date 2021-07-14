using LogoFX.Client.Mvvm.Model.Contracts;

namespace MdClone.Model.Contracts
{
    public interface IEmailRecipientsModel : IAppModel, IEditableModel
    {
        IEmailRecipientModel[] Items { get; set; }
    }
}