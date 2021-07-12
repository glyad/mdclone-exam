namespace MdClone.Data.Contracts.Providers
{
	public interface ISupportedFormatInfo
	{
		SupportedFormats SupportedFormatKey { get; }

		string SupportedFormatName { get; }

		string[] AllowedFileExtensions { get; }
	}
}