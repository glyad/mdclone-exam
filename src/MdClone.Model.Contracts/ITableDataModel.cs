namespace MdClone.Model.Contracts
{
    public interface ITableDataModel : IAppModel
    {
        string[] Header { get; }

        IRowDataModel[] Rows { get; }
    }
}