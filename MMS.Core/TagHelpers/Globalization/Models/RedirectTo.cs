namespace MMS.Core.TagHelpers.Globalization.Models {
	/// <summary>
	/// Defines where to redirect when language is changes
	/// </summary>
	public enum RedirectTo {
		/// <summary>
		/// redirects to home page in the project root
		/// </summary>
		HomePage,
		/// <summary>
		/// redirects to the same page and keep all filter like search
		/// </summary>
		SamePage,
		/// <summary>
		/// redirect to the same page and clear all filters (QueryString) values
		/// </summary>
		SamePageNoQueryString
	}
}
