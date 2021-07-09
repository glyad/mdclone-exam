using MdClone.Model.Contracts;

namespace MdClone.Model
{
    public class DataService : IDataService
    {
        public DataService()
        {

        }

        ITableDataModel IDataService.CreateNewTable()
        {
            return new TableDataModel();
        }

        IEmailModel IDataService.CreateNewEmail()
        {
            return new EmailModel();
        }
    }
}