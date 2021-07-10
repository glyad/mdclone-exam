namespace MdClone.Model.Contracts
{
    public interface IFileTypeModel : IAppModel
    {
        string Filter { get; }

        string DisplayName { get; }
    }
}