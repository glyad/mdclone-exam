namespace MdClone.Data.Contracts.Providers
{
    internal sealed class SupportedFormatInfo : ISupportedFormatInfo
    {
        public SupportedFormats SupportedFormatKey { get; set; }
        
        public string SupportedFormatName { get; set; }

        public string[] AllowedFileExtensions { get; set; }
    }
}