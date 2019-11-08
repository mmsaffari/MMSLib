using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.Alerts {

	/// <summary>
	/// Alert Style bases on bootstrap alerts.
	/// </summary>
	internal enum AlertStyles {
		/// <summary>
		/// bootstraps' alert-primary
		/// </summary>
		Primary,
		/// <summary>
		/// bootstraps' alert-secondary
		/// </summary>
		Secondary,
		/// <summary>
		/// bootstraps' alert-success
		/// </summary>
		Success,
		/// <summary>
		/// bootstraps' alert-danger
		/// </summary>
		Danger,
		/// <summary>
		/// bootstraps' alert-warning
		/// </summary>
		Warning,
		/// <summary>
		/// bootstraps' alert-info
		/// </summary>
		Info,
		/// <summary>
		/// bootstraps' alert-light
		/// </summary>
		Light,
		/// <summary>
		/// bootstraps' alert-dark
		/// </summary>
		Dark
	}
}
