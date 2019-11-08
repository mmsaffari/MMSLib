using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.Globalization.Models {
	/// <summary>
	/// The label to display for language dropdown list on language names
	/// </summary>
	public enum LanguageLabel {
		/// <summary>
		/// Culture name
		/// </summary>
		Name,
		/// <summary>
		/// Culture display name
		/// </summary>
		DisplayName,
		/// <summary>
		/// Culture English name
		/// </summary>
		EnglishName,
		/// <summary>
		/// Culture native name
		/// </summary>
		NativeName,
		/// <summary>
		/// Two letter ISO language name
		/// </summary>
		TwoLetterISOLanguageName
	}
}
