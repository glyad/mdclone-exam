using System.Collections.Generic;
using MdClone.Data.Contracts.Dto;

namespace MdClone.Data.Contracts.Providers
{
	public interface IDataProvider
	{
		IEnumerable<ISupportedFormatInfo> GetSupportedFormats();

        TableDataDto LoadData(string filename);

        void SendEmail(EmailDto emailDto);
    }
}