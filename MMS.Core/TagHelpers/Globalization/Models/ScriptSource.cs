using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.Globalization.Models {
	/// <summary>
	/// The location to load localization valdiation scripts from
	/// </summary>
	public enum ScriptSource {
		/// <summary>
		/// Valdiation scripts are loaded from wwwroot/lib folder
		/// </summary>
		Local,

		/// <summary>
		/// Valdiation scripts will be loaded from jsdelivr
		/// </summary>
		JsDeliver
	}
}
