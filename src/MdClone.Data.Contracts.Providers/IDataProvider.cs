using MdClone.Data.Contracts.Dto;

namespace MdClone.Data.Contracts.Providers
{
    public interface IDataProvider
    {
        TableDataDto LoadData(string filename);

        void SendEmail(EmailDto emailDto);
    }
}