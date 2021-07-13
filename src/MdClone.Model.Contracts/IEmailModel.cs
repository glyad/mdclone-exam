using LogoFX.Client.Mvvm.Model.Contracts;

namespace MdClone.Model.Contracts
{
    public interface IEmailModel : IAppModel, IEditableModel
    {
        IEmailRecipientModel[] To { get; set; }

        IEmailRecipientModel[] Cc { get; set; }

        string Subject { get; set; }

        byte[] Message { get; set; }
    }
}