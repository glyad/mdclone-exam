namespace MdClone.Data.Real.Providers
{
	internal interface IDataReader
	{
		string[] Header { get; }

		DataRow[] Rows { get; }
	}
}