using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.Alerts {
	/// <summary>
	/// Alert item that can be created in the backend manually for pushing alert to themp data
	/// </summary>
	internal class AlertModel {
		/// <summary>
		/// Key to find alerts in TempData dictionary
		/// </summary>
		public const string TempDataKey = "AlertTempKey";

		/// <summary>
		/// Alert style depending on Bootstrap 4.x classes
		/// </summary>
		public AlertStyles Style { get; set; } = AlertStyles.Primary;

		/// <summary>
		/// Header text for the alert message
		/// </summary>
		public string Heading { get; set; }

		/// <summary>
		/// Alert message body
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// true for dismissable alert
		/// </summary>
		public bool Dismissable { get; set; } = true;
	}
}
