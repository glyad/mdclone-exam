namespace MdClone.Model.Contracts
{
    public interface IDataService
    {
        ITableDataModel CreateNewTable();

        IEmailModel CreateNewEmail();

        IFileTypeModel[] FileTypes { get; }
    }
}