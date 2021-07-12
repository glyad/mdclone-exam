using LogoFX.Client.Mvvm.Model.Contracts;

namespace MdClone.Model.Contracts
{
    public interface IEmailModel : IAppModel, IEditableModel
    {
        string To { get; set; }

        string Cc { get; set; }

        string Subject { get; set; }

        byte[] Message { get; set; }
    }
}