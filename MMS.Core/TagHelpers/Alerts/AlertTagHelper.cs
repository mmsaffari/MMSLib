using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.TagHelpers.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers.Alerts {
	/// <summary>
	/// Alert messages with bootstrap 4.x styles
	/// </summary>
	public class AlertTagHelper : TagHelper {
		internal AlertStyles Style { get; set; } = AlertStyles.Primary;

		/// <summary>
		/// Heading text for the alert
		/// </summary>
		public string Heading { get; set; }

		/// <summary>
		/// <para>Is the alert dismissible?</para>
		/// <para>Default is true.</para>
		/// </summary>
		public bool Dismissable { get; set; } = true;

		/// <summary>
		/// View context to access TempData, mainly to get alerts originating in the backend.
		/// </summary>
		[ViewContext]
		public ViewContext ViewContext { get; set; } = null;

		/// <summary>
		/// Create alert messages styled with bootstrap 4.x
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			output.TagName = "div";
			
			if (ViewContext != null) {
				var alerts = ViewContext.TempData.ContainsKey(AlertModel.TempDataKey)
					//? JsonConvert.DeserializeObject<List<Alert>>(ViewContext.TempData[Alert.TempDataKey].ToString())
					//? (List<Alert>)ViewContext.TempData[Alert.TempDataKey]
					? ViewContext.TempData.Get<List<AlertModel>>(AlertModel.TempDataKey)
					: new List<AlertModel>();

				alerts.ForEach(x => output.Content.AppendHtml(AddAlert(x)));

				ViewContext.TempData.Remove(AlertModel.TempDataKey);
			}

			// read alerts contents from inner html
			var msg = await output.GetChildContentAsync();

			if (!string.IsNullOrWhiteSpace(msg.GetContent())) {
				var manualAlert = AddAlert(new AlertModel {
					Heading = this.Heading,
					Message = msg.GetContent(),
					Style = this.Style,
					Dismissable = this.Dismissable
				});
				output.Content.AppendHtml(manualAlert);
			}

		}

		private TagBuilder AddAlert(AlertModel alert) {
			var _alert = new TagBuilder("div");

			var alertStyle = Enum.GetName(typeof(AlertStyles), alert.Style).ToLower();
			_alert.AddCssClass($"alert alert-{alertStyle}");
			_alert.Attributes.Add("role", "alert");

			if (alert.Dismissable) {
				_alert.InnerHtml.AppendHtml("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>");
			}

			if (!string.IsNullOrWhiteSpace(alert.Heading)) {
				_alert.InnerHtml.AppendHtml($"<h4 class='alert-heading'>{alert.Heading}</h4>");
			}

			if (!string.IsNullOrWhiteSpace(alert.Message)) {
				_alert.InnerHtml.AppendHtml($"<p class='mb-0'>{alert.Message}</p>");
			}

			return _alert;
		}
	}
}
