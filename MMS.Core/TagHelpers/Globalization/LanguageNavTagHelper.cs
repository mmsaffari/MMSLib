using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MMS.Core.TagHelpers.Globalization.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MMS.Core.TagHelpers.Globalization {
	/// <summary>
	/// Creates a language-selection navigation menu based on supported cultures
	/// </summary>
	public class LanguageNavTagHelper : TagHelper {
		/// <summary>
		/// Optional: Route data key name for the culture part, default "culture"
		/// Default: cultute
		/// </summary>
		public string CultureKeyName { get; set; } = "culture";

		/// <summary>
		/// Optional: Manually specify a comma-separated list of supported cultures
		/// <example>
		/// en-US, fa-IR, de, fr
		/// </example>
		/// </summary>
		public string SupportedCultures { get; set; }

		/// <summary>
		/// Optional: The part of culture to show as menu items' label.
		/// Default: LanguageLabel.EnglishName
		/// </summary>
		public LanguageLabel LanguageLabel { get; set; } = LanguageLabel.EnglishName;

		/// <summary>
		/// Optional: Specify the redirect policy for when the language is changed
		/// Default value: RedirectTo.SamePage
		/// </summary>
		public RedirectTo RedirectTo { get; set; } = RedirectTo.SamePage;

		/// <summary>
		/// Optinal: The URL part after the culture when RedirectTo is HomePage
		/// <example>/en/Home/Index</example>
		/// </summary>
		public string HomePageName { get; set; } = "";

		/// <summary>
		/// Current view context
		/// </summary>
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		/// <summary>
		/// Render mode: Classic for an HTML dropdown list, Bootstrap for HTML5 div with Bootstrap4 style (which is the default).
		/// </summary>
		public Models.RenderMode RenderMode { get; set; } = Models.RenderMode.Bootstrap;

		/// <summary>
		/// required for listing supported cultures
		/// </summary>
		private readonly IOptions<RequestLocalizationOptions> _reqLocOpts;
		private readonly ILogger _logger;
		private readonly IOptions<MvcOptions> _mvdOpts;
		private readonly LinkGenerator _linkGen;

		/// <summary>
		/// Creates a language navigation menu, depends on supported cultures
		/// Uses Property Injection to receive Logger and stuff.
		/// </summary>
		/// <param name="logger">Logger facility</param>
		/// <param name="requestLocalizationOptions">Request localization options</param>
		/// <param name="linkGenerator">link generator</param>
		/// <param name="mvcOptions">MVC Options</param>
		public LanguageNavTagHelper(ILogger<LanguageNavTagHelper> logger, IOptions<RequestLocalizationOptions> requestLocalizationOptions, LinkGenerator linkGenerator, IOptions<MvcOptions> mvcOptions) {
			_logger = logger;
			_reqLocOpts = requestLocalizationOptions;
			_linkGen = linkGenerator;
			_mvdOpts = mvcOptions;
		}

		/// <summary>
		/// Created the language navigation element
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		public override void Process(TagHelperContext context, TagHelperOutput output) {
			var langDictionary = CreateNavDictionary();

			if (RenderMode == Models.RenderMode.Bootstrap) {
				CreateBootstrapItems(ref output, langDictionary);
			} else {
				CreateClassicItems(ref output, langDictionary);
			}
		}

		/// <summary>
		/// Creates <b>classic</b> list items.
		/// <example><![CDATA[<option value="/en-US">English</option>]]></example>
		/// </summary>
		/// <param name="languages">A list of <see cref="LanguageItem"/>s</param>
		/// <param name="output">Generated TagHelperOuput</param>
		/// <returns></returns>
		private void CreateClassicItems(ref TagHelperOutput output, List<LanguageItem> languages) {
			output.TagName = "select";
			foreach (var lang in languages.OrderBy(x => x.DisplayText)) {
				var option = new TagBuilder("option");
				option.Attributes.Add("value", lang.Url);
				option.InnerHtml.AppendHtml(lang.DisplayText);

				if (CultureInfo.CurrentCulture.Name == lang.Name)
					option.Attributes.Add("selected", "selected");

				output.Content.AppendHtml(option);
			}
		}

		/// <summary>
		/// Create <b>modern</b> list items.
		/// <example><![CDATA[<a href="/en-US" class="itemxyz">English</a>]]></example>
		/// </summary>
		/// <param name="languages">A list of <see cref="LanguageItem"/>s</param>
		/// <param name="output">Generated TagHelperOuput</param>
		/// <returns></returns>
		private void CreateBootstrapItems(ref TagHelperOutput output, List<LanguageItem> languages) {
			var div = new TagBuilder("div");

			if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
				div.AddCssClass("dropdown-menu dropdown-menu-left");
			else
				div.AddCssClass("dropdown-menu dropdown-menu-right");

			div.Attributes.Add("aria-labeledby", "dropdownlang");

			foreach (var lang in languages.Where(x => x.Name != CultureInfo.CurrentCulture.Name).OrderBy(x => x.DisplayText)) {
				var a = new TagBuilder("a");
				a.AddCssClass("dropdown-item small");
				a.Attributes.Add("href", lang.Url);
				a.InnerHtml.Append(lang.DisplayText);

				div.InnerHtml.AppendHtml(a);
			}

			output.TagName = "div";
			output.Attributes.Add("class", "dropdown");

			var toggle = CreateToggle();
			output.Content.AppendHtml(toggle);

			output.Content.AppendHtml(div);
		}

		/// <summary>
		/// Creates a list of all supported cultures
		/// </summary>
		/// <returns></returns>
		private List<LanguageItem> CreateNavDictionary() {
			var _routeData = CreateRouteDataDictionary();

			// if we are redirecting to the home page, then we need
			// only culture paramter and home page url part in route values
			if (RedirectTo == RedirectTo.HomePage) {
				ViewContext.RouteData.Values.Clear();
				ViewContext.RouteData.Values.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);
				ViewContext.RouteData.Values.Add("page", $"/{HomePageName}");
			}

			var result = new List<LanguageItem>();
			var urlHelper = new UrlHelper(ViewContext);
			var cultures = GetSupportedCultures();

			foreach (var cul in cultures) {
				//replace culture value with the relevant one for dropdown list
				_routeData[CultureKeyName] = cul.Name;

				var urlRoute = new UrlRouteContext { Values = _routeData };

				string url;

				// DotNetCore 3.0 has optional value to use EndPointRouting
				// First check if EndPointRouting is enabled
				// Or use generic urlHelper to generate url
				url = _mvdOpts.Value.EnableEndpointRouting
					? _linkGen.GetPathByRouteValues(httpContext: ViewContext.HttpContext, "", _routeData)
					: urlHelper.RouteUrl(urlRoute);
				var label = GetLanguageLabel(cul);
				result.Add(new LanguageItem { Name = cul.Name, DisplayText = label, Url = url });
			}

			return result;
		}

		/// <summary>
		/// Returns a <![CDATA[Dictionary<string, object>]]> representing route data values for the current route
		/// </summary>
		private Dictionary<string, object> CreateRouteDataDictionary() {
			var result = new Dictionary<string, object>();

			if (RedirectTo == RedirectTo.HomePage)
				result.Add(CultureKeyName, CultureInfo.CurrentCulture.Name);
			else {
				//redirect to same page or same page without query string
				foreach (var r in ViewContext.RouteData.Values) {
					result.Add(r.Key, r.Value);
				}

				if (RedirectTo == RedirectTo.SamePage) {
					foreach (var q in ViewContext.HttpContext.Request.Query) {
						result.Add(q.Key, q.Value);
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Returns a list of supported CultureInfos
		/// </summary>
		private IEnumerable<CultureInfo> GetSupportedCultures() {
			// if the user didn't specify manually list of supported cultures, 
			// then create cultures list with reference to supported cultures defined in localization settings in startup</para>
			if (string.IsNullOrWhiteSpace(SupportedCultures))
				return _reqLocOpts.Value.SupportedCultures;

			//if the user will specify supported cultures manually, then this list will be created accordingly
			var result = new List<CultureInfo>();
			foreach (var c in SupportedCultures.Split(new[] { ',', '|', ';', ' ' }, System.StringSplitOptions.RemoveEmptyEntries)) {
				result.Add(new CultureInfo(c));
			}

			return result;
		}

		private string GetLanguageLabel(CultureInfo cul) {
			switch (LanguageLabel) {
				case LanguageLabel.Name: return cul.Name;
				case LanguageLabel.DisplayName: return cul.DisplayName;
				case LanguageLabel.EnglishName: return cul.EnglishName;
				case LanguageLabel.NativeName: return cul.NativeName;
				case LanguageLabel.TwoLetterISOLanguageName: return cul.TwoLetterISOLanguageName;

				default: return cul.EnglishName;
			}
		}

		private TagBuilder CreateToggle() {
			var toggle = new TagBuilder("a");
			toggle.AddCssClass("btn-sm btn-default border border-secondary dropdown-toggle");
			toggle.Attributes.Add("id", "dropdownLang");
			toggle.Attributes.Add("href", "#");
			toggle.Attributes.Add("role", "button");
			toggle.Attributes.Add("data-toggle", "dropdown");
			toggle.Attributes.Add("aria-haspopup", "true");
			toggle.Attributes.Add("aria-expanded", "false");

			var label = GetLanguageLabel(CultureInfo.CurrentCulture);
			toggle.InnerHtml.Append(label);

			return toggle;
		}
	}
}
