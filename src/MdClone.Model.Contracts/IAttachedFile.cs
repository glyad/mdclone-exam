namespace MdClone.Model.Contracts
{
    public interface IAttachedFile : IAppModel
    {
        long FileSize { get; }
    }
}