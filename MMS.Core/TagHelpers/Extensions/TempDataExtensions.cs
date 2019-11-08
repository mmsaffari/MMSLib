using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MMS.Core.TagHelpers.Alerts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MMS.Core.TagHelpers.Extensions {
	/// <summary>
	/// Extension methods used to add complex object and fix json's serialization problem
	/// </summary>
	/// <![CDATA[https://stackoverflow.com/a/35042391/5519026]]>
	public static class TempDataExtensions {

		#region TempData Management methods
		/// <summary>
		/// Puts an object with a key into the TempData dictionary
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class {
			tempData[key] = JsonSerializer.Serialize(value);
		}

		/// <summary>
		/// Gets an object from the TempData using its key.
		/// Retruns null if the objects does not exist.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class {
			tempData.TryGetValue(key, out object o);
			var obj = JsonSerializer.Deserialize<T>((string)o);
			return o == null ? null : obj;
		}

		/// <summary>
		/// Returns an object that contains the element that is 
		/// associated with the specified key, without marking the key
		/// for deletion.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class {
			object o = tempData.Peek(key);
			return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
		}
		#endregion

		#region Bootstrap Alert Methods
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
			var alerts = tempData.ContainsKey(Alert.TempDataKey)
				? JsonSerializer.Deserialize<List<Alert>>(tempData[Alert.TempDataKey].ToString())
				: new List<Alert>();
			alerts.Add(new Alert {
				Style = style,
				Heading = header,
				Message = message,
				Dismissable = dismissable
			});

			tempData.Put(Alert.TempDataKey, alerts);
		}
		#endregion

	}
}
