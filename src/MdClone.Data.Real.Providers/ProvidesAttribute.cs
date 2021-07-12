using System;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
	
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal class ProvidesAttribute : Attribute
	{
		
		internal ProvidesAttribute(SupportedFormats format, params string[] fileExtensions)
		{
			Format = format;
			FileExtensions = fileExtensions;
		}

		internal SupportedFormats Format { get; } 

		internal string[] FileExtensions { get; }
	}

}