using LogoFX.Client.Mvvm.Model.Contracts;

namespace MdClone.Model.Contracts
{
    public interface IEmailRecipientModel : IAppModel, IEditableModel
    {
        string Address { get; set; }
    }
}