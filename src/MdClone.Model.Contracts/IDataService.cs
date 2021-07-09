namespace MdClone.Model.Contracts
{
    public interface IDataService
    {
        ITableDataModel CreateNewTable();

        IEmailModel CreateNewEmail();
    }
}