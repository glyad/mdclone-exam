namespace MdClone.Data.Real.Providers
{
    internal sealed class CsvRow
    {
        public CsvRow(string[] values)
        {
            Values = values;
        }

        public string[] Values { get; }
    }
}