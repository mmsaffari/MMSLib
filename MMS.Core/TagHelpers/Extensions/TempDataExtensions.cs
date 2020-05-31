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

	}
}
