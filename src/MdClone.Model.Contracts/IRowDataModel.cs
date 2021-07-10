namespace MdClone.Model.Contracts
{
    public interface IRowDataModel : IAppModel
    {
        IItemDataModel[] Items { get; }
    }
}