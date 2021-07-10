namespace MdClone.Model.Contracts
{
    public interface IItemDataModel : IAppModel
    {
        string Header { get; }

        string Value { get; }
    }
}