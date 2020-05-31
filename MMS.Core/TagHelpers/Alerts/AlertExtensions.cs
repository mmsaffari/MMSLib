using Microsoft.AspNetCore.Mvc.ViewFeatures;

using MMS.Core.TagHelpers.Extensions;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MMS.Core.TagHelpers.Alerts {
	/// <summary>
	/// Extensions for TempData for creating coder friendly alerts easily
	/// </summary>
	public static class TempDataExtensions {
		/// <summary>
		/// Create primary alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Primary(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Primary, message, header, dismissable);
		}

		/// <summary>
		/// Create secondary alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Secondary(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Secondary, message, header, dismissable);
		}

		/// <summary>
		/// Create success alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Success(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Success, message, header, dismissable);
		}

		/// <summary>
		/// Create danger alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Danger(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Danger, message, header, dismissable);
		}

		/// <summary>
		/// Create warning alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Warning(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Warning, message, header, dismissable);
		}

		/// <summary>
		/// Create info alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Info(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Info, message, header, dismissable);
		}

		/// <summary>
		/// Create light alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Light(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Light, message, header, dismissable);
		}

		/// <summary>
		/// Create dark alert
		/// </summary>
		/// <param name="tempData">TempData</param>
		/// <param name="message">Message body</param>
		/// <param name="header">Message header</param>
		/// <param name="dismissable">Show closing button</param>
		public static void Dark(this ITempDataDictionary tempData, string message, string header = "", bool dismissable = true) {
			AddAlert(tempData, AlertStyles.Dark, message, header, dismissable);
		}

		private static void AddAlert(ITempDataDictionary tempData, AlertStyles style, string message, string header, bool dismissable) {
			var alerts = tempData.ContainsKey(AlertModel.TempDataKey)
				? JsonSerializer.Deserialize<List<AlertModel>>(tempData[AlertModel.TempDataKey].ToString())
				: new List<AlertModel>();
			alerts.Add(new AlertModel {
				Style = style,
				Heading = header,
				Message = message,
				Dismissable = dismissable
			});

			tempData.Put(AlertModel.TempDataKey, alerts);
		}
	}
}
