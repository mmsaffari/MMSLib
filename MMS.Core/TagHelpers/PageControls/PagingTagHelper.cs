using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMS.Core.TagHelpers.PageControls {
	/// <summary>
	/// Creates a pagination control
	/// </summary>
	public class PagingTagHelper : TagHelper {
		private IConfiguration Configuration { get; }
		private ILogger _logger;

		/// <summary>
		/// Creates a PagingTagHelper and injects Configuration and Logger properties.
		/// </summary>
		public PagingTagHelper(IConfiguration configuration, ILogger<PagingTagHelper> logger) {
			Configuration = configuration;
			_logger = logger;
		}

		#region Settings

		/// <summary>
		/// Current page number
		/// <para>Default: 1</para>
		/// <para>Example: p=1</para>
		/// </summary>
		public int PageNo { get; set; }

		/// <summary>
		/// Number of records shown on each page
		/// <para>Default: 25</para>
		/// <para>Example: pagesize=25</para>
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Total record count
		/// <para>Default: 0</para>
		/// </summary>
		public int TotalRecords { get; set; }

		/// <summary>
		/// If the count of pages is more than this attribute, show only this number of pages with numbered buttons and show an ellipsis for other pages.
		/// <para>Default: 10</para>
		/// </summary>
		public int MaxDisplayedPages { get; set; }

		/// <summary>
		/// Gap size to start show first/last numbered page
		/// <para>Default: 5</para>
		/// </summary>
		public int GapSize { get; set; }

		/// <summary>
		/// Name of the paging section in <b>MMS:Paging</b> section inside appSettings.json
		/// <param>Default: "default"</param>
		/// </summary>
		public string SettingsJson { get; set; }

		#endregion

		#region Page Size Navigation
		/// <summary>
		/// Form submit method when selecting different page size option
		/// <para>Default: get</para>
		/// <para>Options: get, post</para>
		/// </summary>
		public string PageSizeNavFormMethod { get; set; }

		/// <summary>
		/// The minimum block size to populate all possible page sizes for dropdown list
		/// <para>Default: 25</para>
		/// </summary>
		public int PageSizeNavBlockSize { get; set; }

		/// <summary>
		/// Maximum nmber of items to show in the page size navigation menu
		/// <para>Default: 5</para>
		/// </summary>
		public int PageSizeNavMaxItems { get; set; }

		/// <summary>
		/// Action to execute when page size dropdown changes
		/// <para>Default: this.form.submit();</para>
		/// </summary>
		public string PageSizeNavOnChange { get; set; }

		#endregion

		#region QueryString

		/// <summary>
		/// Query string paramter name for current page.
		/// <para>Default: p</para>
		/// <para>exmaple: p=1</para>
		/// </summary>
		public string QueryStringKeyPageNo { get; set; }

		/// <summary>
		/// Query string parameter name for page size
		/// <para>Default: s</para>
		/// <para>Example: s=25</para>
		/// </summary>
		public string QueryStringKeyPageSize { get; set; }

		/// <summary>
		/// Query string value starting from the ? including all next query string parameters 
		/// to consider for next pages links.
		/// <para>Default: string.Empty</para>
		/// <example>
		/// <code>
		/// @(Request.QueryString.Value)
		/// </code>
		/// </example>
		/// </summary>
		public string QueryStringValue { get; set; }

		#endregion

		#region Display Settings

		/// <summary>
		/// Show drop down list for different page size options
		/// <para>Default: false</para>
		/// <para>Options: true, false</para>
		/// </summary>
		public bool? ShowPageSizeNav { get; set; }

		/// <summary>
		/// Show or hide First and Last page buttons
		/// <para>Default: false, but will auto show if total pages > max displayed pages</para>
		/// </summary>
		public bool? ShowFirstLast { get; set; }

		/// <summary>
		/// Show or hide Previous and Next page buttons
		/// <para>Default: false</para>
		/// </summary>
		public bool? ShowPrevNext { get; set; }

		/// <summary>
		/// Show or hide total pages count
		/// <para>Default: false</para>
		/// </summary>
		public bool? ShowTotalPages { get; set; }

		/// <summary>
		/// Show or hide total records count
		/// <para>Default: false</para>
		/// </summary>
		public bool? ShowTotalRecords { get; set; }

		/// <summary>
		/// Show last numbered page when total pages count is larger than max displayed pages
		/// <para>Default: false</para>
		/// </summary>
		public bool? ShowLastNumberedPage { get; set; }

		/// <summary>
		/// Show first numbered page when total pages count is larger than max displayed pages
		/// <para>Default: false</para>
		/// </summary>
		public bool? ShowFirstNumberedPage { get; set; }

		#endregion

		#region Texts
		/// <summary>
		/// The text to display at page size dropdown list label
		/// <para>Default: Items per page </para>
		/// </summary>
		public string TextPageSize { get; set; }


		/// <summary>
		/// Text to show on the "Go To First" Page button
		/// <para>
		/// <![CDATA[Default: &laquo;]]></para>
		/// </summary>
		public string TextFirst { get; set; }

		/// <summary>
		/// Text to show on "Go to last page" button
		/// <para>
		/// <![CDATA[Default: &raquo;]]></para>
		/// </summary>
		public string TextLast { get; set; }

		/// <summary>
		/// Next button text
		/// <para>
		/// <![CDATA[Default: &rsaquo;]]></para>
		/// </summary>
		public string TextNext { get; set; }

		/// <summary>
		/// previous button text
		/// <para>
		/// <![CDATA[Default: &lsaquo;]]></para>
		/// </summary>
		public string TextPrevious { get; set; }

		/// <summary>
		/// Display text for total pages label
		/// <para>Default: pages</para>
		/// </summary>
		public string TextTotalPages { get; set; }

		/// <summary>
		/// Display text for total records label
		/// <para>Default: records</para>
		/// </summary>
		public string TextTotalRecords { get; set; }
		#endregion

		#region Screen Reader
		/// <summary>
		/// Text for screen readers only on First button
		/// </summary>
		public string SrTextFirst { get; set; }

		/// <summary>
		/// Text for screen readers only on Last button
		/// </summary>
		public string SrTextLast { get; set; }

		/// <summary>
		/// Text for screenreaders only on Next button
		/// </summary>
		public string SrTextNext { get; set; }

		/// <summary>
		/// Text for screen readers only on Previous button
		/// </summary>
		public string SrTextPrevious { get; set; }

		#endregion

		#region Styling

		/// <summary>
		/// Add custom class to content div
		/// </summary>
		public string Class { get; set; }

		/// <summary>
		/// CSS class for pagination div
		/// </summary>
		public string ClassPagingControlDiv { get; set; }

		/// <summary>
		/// CSS class for page count/record count div
		/// </summary>
		public string ClassInfoDiv { get; set; }

		/// <summary>
		/// CSS class for page size div
		/// </summary>
		public string ClassPageSizeDiv { get; set; }

		/// <summary>
		/// CSS class for pagination control
		/// <para>Default: pagination</para>
		/// </summary>
		public string ClassPagingControl { get; set; }

		/// <summary>
		/// CSS class name for the active page button
		/// <para>Default: active</para>
		/// <para>examples: disabled, active, ...</para>
		/// </summary>
		public string ClassActivePage { get; set; }

		/// <summary>
		/// name of the CSS class when jumping button is disabled.
		/// jumping buttons are prev-next and first-last buttons
		/// <param>Default: disabled</param>
		/// <para>Example: disabled, d-hidden</para>
		/// </summary>
		public string ClassDisabledJumpingButton { get; set; }

		/// <summary>
		/// CSS class for total records info
		/// <para>Default: badge badge-light</para>
		/// </summary>
		public string ClassTotalRecords { get; set; }

		/// <summary>
		/// CSS class for total pages info
		/// <para>Default: badge badge-light</para>
		/// </summary>
		public string ClassTotalPages { get; set; }

		#endregion

		private int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

		private class Boundaries {
			public int Start { get; set; }
			public int End { get; set; }
		}

		/// <summary>
		/// process the tag helper creating the paging control
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		public override void Process(TagHelperContext context, TagHelperOutput output) {
			SetDefaults();

			if (TotalPages > 0) {
				var pagingControl = new TagBuilder("ul");
				pagingControl.AddCssClass($"{ClassPagingControl}");

				// if first/last buttons are not enabled, but the total pages more than displayed pages
				// then show jumping buttons automatically
				if (ShowFirstLast != true && TotalPages > MaxDisplayedPages) {
					ShowFirstLast = true;
				}

				if (ShowFirstLast == true) {
					var first = CreatePagingLink(1, TextFirst, SrTextFirst, ClassDisabledJumpingButton);
					pagingControl.InnerHtml.AppendHtml(first);
				}

				if (ShowPrevNext == true) {
					var prevPage = PageNo - 1 <= 1 ? 1 : PageNo - 1;
					var prev = CreatePagingLink(prevPage, TextPrevious, SrTextPrevious, ClassDisabledJumpingButton);
					pagingControl.InnerHtml.AppendHtml(prev);
				}

				var boundaries = CalculateBoundaries(PageNo, TotalPages, MaxDisplayedPages);

				if (ShowFirstNumberedPage == true
					&& boundaries.Start > GapSize
					&& TotalPages > MaxDisplayedPages
					&& PageNo >= MaxDisplayedPages) {
					var numTag = CreatePagingLink(1, null, null, ClassActivePage);
					pagingControl.InnerHtml.AppendHtml(numTag);

					var gap = new TagBuilder("li");
					gap.AddCssClass("page-item border-0");
					gap.InnerHtml.AppendHtml("&nbsp;...&nbsp;");
					pagingControl.InnerHtml.AppendHtml(gap);
				}

				for (int i = boundaries.Start; i <= boundaries.End; i++) {
					var numTag = CreatePagingLink(i, null, null, ClassActivePage);
					pagingControl.InnerHtml.AppendHtml(numTag);
				}

				if (ShowLastNumberedPage == true
					&& TotalPages - boundaries.End >= GapSize
					&& PageNo - GapSize <= TotalPages - MaxDisplayedPages) {
					var gap = new TagBuilder("li");
					gap.AddCssClass("page-item border-0");
					gap.InnerHtml.AppendHtml("&nbsp;...&nbsp;");
					pagingControl.InnerHtml.AppendHtml(gap);

					var numTag = CreatePagingLink(TotalPages, null, null, ClassActivePage);
					pagingControl.InnerHtml.AppendHtml(numTag);
				}

				if (ShowPrevNext == true) {
					var nextPage = PageNo + 1 > TotalPages ? TotalPages : PageNo + 1;
					var next = CreatePagingLink(nextPage, TextNext, SrTextNext, ClassDisabledJumpingButton);
					pagingControl.InnerHtml.AppendHtml(next);
				}

				if (ShowFirstLast == true) {
					var last = CreatePagingLink(TotalPages, TextLast, SrTextLast, ClassDisabledJumpingButton);
					pagingControl.InnerHtml.AppendHtml(last);
				}

				var pagingControlDiv = new TagBuilder("div");
				pagingControlDiv.AddCssClass($"{ClassPagingControlDiv}");
				pagingControlDiv.InnerHtml.AppendHtml(pagingControl);

				output.TagName = "div";
				output.Attributes.SetAttribute("class", $"{Class}");
				output.Content.AppendHtml(pagingControlDiv);

				if (ShowTotalPages == true || ShowTotalRecords == true) {
					var infoDiv = new TagBuilder("div");
					infoDiv.AddCssClass($"{ClassInfoDiv}");

					if (ShowTotalPages == true) {
						var totalPagesInfo = AddDisplayInfo(TotalPages, TextTotalPages, ClassTotalPages);
						infoDiv.InnerHtml.AppendHtml(totalPagesInfo);
					}

					if (ShowTotalRecords == true) {
						var totalRecordsInfo = AddDisplayInfo(TotalRecords, TextTotalRecords, ClassTotalRecords);
						infoDiv.InnerHtml.AppendHtml(totalRecordsInfo);
					}

					output.Content.AppendHtml(infoDiv);
				}

				if (ShowPageSizeNav == true) {
					var psDropdown = CreatePageSizeControl();

					var psDiv = new TagBuilder("div");
					psDiv.AddCssClass($"{ClassPageSizeDiv}");
					psDiv.InnerHtml.AppendHtml(psDropdown);

					output.Content.AppendHtml(psDiv);
				}
			}
		}

		/// <summary>
		/// This method will assign the values by checking three places
		/// 1- Attribute value if set on the tag in HTML code
		/// 2- Default values if set in appSettings.json
		/// 3- Hard-coded default values in this method
		/// </summary>
		private void SetDefaults() {
			var _settingsJson = SettingsJson ?? "default";

			_logger.LogInformation($"----> PagingTagHelper SettingsJson: {SettingsJson} - {_settingsJson}");

			PageNo = PageNo > 1 ? PageNo :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:page-no"], out int _pn) ? _pn : 1;

			PageSize = PageSize > 0 ? PageSize :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:page-size"], out int _ps) ? _ps : 10;

			TotalRecords = TotalRecords > 0 ? TotalRecords :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:total-records"], out int _tr) ? _tr : 0;

			MaxDisplayedPages = MaxDisplayedPages > 0 ? MaxDisplayedPages :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:max-displayed-pages"], out int _dp) ? _dp : 10;

			GapSize = GapSize > 0 ? GapSize :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:gap-size"], out int _gap) ? _gap : 3;

			PageSizeNavFormMethod = PageSizeNavFormMethod ?? Configuration[$"MMS:Paging:{_settingsJson}:page-size-nav-form-method"] ?? "get";

			PageSizeNavBlockSize = PageSizeNavBlockSize > 0 ? PageSizeNavBlockSize :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:page-size-nav-block-size"], out int _bs) ? _bs : 10;

			PageSizeNavMaxItems = PageSizeNavMaxItems > 0 ? PageSizeNavMaxItems :
				int.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:page-size-nav-max-items"], out int _mi) ? _mi : 3;

			PageSizeNavOnChange = PageSizeNavOnChange ?? Configuration[$"MMS:Paging:{_settingsJson}:page-size-nav-on-change"] ?? "this.form.submit();";

			QueryStringKeyPageNo = QueryStringKeyPageNo ?? Configuration[$"MMS:Paging:{_settingsJson}:query-string-key-page-no"] ?? "p";

			QueryStringKeyPageSize = QueryStringKeyPageSize ?? Configuration[$"MMS:Paging:{_settingsJson}:query-string-key-page-size"] ?? "s";

			QueryStringValue = QueryStringValue ?? Configuration[$"MMS:Paging:{_settingsJson}:query-string-value"] ?? "";

			ShowFirstLast = ShowFirstLast == null ?
				bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-first-last"], out bool _sfl) ? _sfl : false : ShowFirstLast;

			ShowPrevNext = ShowPrevNext == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-prev-next"], out bool _sprn) ? _sprn : false : ShowPrevNext;

			ShowPageSizeNav = ShowPageSizeNav == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-page-size-nav"], out bool _spsn) ? _spsn : false : ShowPageSizeNav;

			ShowTotalPages = ShowTotalPages == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-total-pages"], out bool _stp) ? _stp : false : ShowTotalPages;

			ShowTotalRecords = ShowTotalRecords == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-total-records"], out bool _str) ? _str : false : ShowTotalRecords;

			ShowFirstNumberedPage = ShowFirstNumberedPage == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-first-numbered-page"], out bool _sfp) ? _sfp : false : ShowFirstNumberedPage;

			ShowLastNumberedPage = ShowLastNumberedPage == null ? bool.TryParse(Configuration[$"MMS:Paging:{_settingsJson}:show-last-numbered-page"], out bool _slp) ? _slp : false : ShowLastNumberedPage;

			TextPageSize = TextPageSize ?? Configuration[$"MMS:Paging:{_settingsJson}:text-page-size"] ?? "Items per page";

			TextFirst = TextFirst ?? Configuration[$"MMS:Paging:{_settingsJson}:text-first"] ?? "&laquo;";

			TextLast = TextLast ?? Configuration[$"MMS:Paging:{_settingsJson}:text-last"] ?? "&raquo;";

			TextPrevious = TextPrevious ?? Configuration[$"MMS:Paging:{_settingsJson}:text-previous"] ?? "&lsaquo;";

			TextNext = TextNext ?? Configuration[$"MMS:Paging:{_settingsJson}:text-next"] ?? "&rsaquo;";

			TextTotalPages = TextTotalPages ?? Configuration[$"MMS:Paging:{_settingsJson}:text-total-pages"] ?? "pages";

			TextTotalRecords = TextTotalRecords ?? Configuration[$"MMS:Paging:{_settingsJson}:text-total-records"] ?? "records";

			SrTextFirst = SrTextFirst ?? Configuration[$"MMS:Paging:{_settingsJson}:sr-text-first"] ?? "First";

			SrTextLast = SrTextLast ?? Configuration[$"MMS:Paging:{_settingsJson}:sr-text-last"] ?? "Last";

			SrTextPrevious = SrTextPrevious ?? Configuration[$"MMS:Paging:{_settingsJson}:sr-text-previous"] ?? "Previous";

			SrTextNext = SrTextNext ?? Configuration[$"MMS:Paging:{_settingsJson}:sr-text-next"] ?? "Next";

			Class = Class ?? Configuration[$"MMS:Paging:{_settingsJson}:class"] ?? "row";

			ClassActivePage = ClassActivePage ?? Configuration[$"MMS:Paging:{_settingsJson}:class-active-page"] ?? "active";

			ClassDisabledJumpingButton = ClassDisabledJumpingButton ?? Configuration[$"MMS:Paging:{_settingsJson}:class-disabled-jumping-button"] ?? "disabled";

			ClassInfoDiv = ClassInfoDiv ?? Configuration[$"MMS:Paging:{_settingsJson}:class-info-div"] ?? "col";

			ClassPageSizeDiv = ClassPageSizeDiv ?? Configuration[$"MMS:Paging:{_settingsJson}:class-page-size-div"] ?? "col";

			ClassPagingControlDiv = ClassPagingControlDiv ?? Configuration[$"MMS:Paging:{_settingsJson}:class-paging-control-div"] ?? "col";

			ClassPagingControl = ClassPagingControl ?? Configuration[$"MMS:Paging:{_settingsJson}:class-paging-control"] ?? "pagination";

			ClassTotalPages = ClassTotalPages ?? Configuration[$"MMS:Paging:{_settingsJson}:class-total-pages"] ?? "badge badge-secondary";

			ClassTotalRecords = ClassTotalRecords ?? Configuration[$"MMS:Paging:{_settingsJson}:class-total-records"] ?? "badge badge-info";
#if DEBUG
			_logger.LogInformation($"----> PagingTagHelper - " +
				$"{nameof(PageNo)}: {PageNo}, " +
				$"{nameof(PageSize)}: {PageSize}, " +
				$"{nameof(TotalRecords)}: {TotalRecords}, " +
				$"{nameof(TotalPages)}: {TotalPages}, " +
				$"{nameof(QueryStringKeyPageNo)}: {QueryStringKeyPageNo}, " +
				$"{nameof(QueryStringKeyPageSize)}: {QueryStringKeyPageSize}, " +
				$"{nameof(QueryStringValue)}: {QueryStringValue}" +
				$"");
#endif
		}

		private TagBuilder AddDisplayInfo(int count, string itemName, string cssClassName) {
			var span = new TagBuilder("span");
			span.AddCssClass($"{cssClassName}");
			span.InnerHtml.AppendHtml($"{count.ToString("N0")} {itemName}");

			return span;
		}

		private Boundaries CalculateBoundaries(int currentPageNo, int totalPages, int maxDisplayedPages) {
			var _start = 1;
			var _end = maxDisplayedPages;
			var _gap = (int)Math.Ceiling(maxDisplayedPages / 2.0);

			if (maxDisplayedPages > totalPages)
				maxDisplayedPages = totalPages;

			// << < 1 2 (3) 4 5 6 7 8 9 10 > >>
			if (currentPageNo < maxDisplayedPages) {
				_start = 1;
				_end = maxDisplayedPages;
			}

			// << < 91 92 93 94 95 96 97 (98) 99 100 > >>
			else if (currentPageNo + maxDisplayedPages > totalPages) {
				_start = totalPages - maxDisplayedPages > 0 ? totalPages - maxDisplayedPages : 1;
				_end = totalPages;
			}

			// << < 21 22 23 34 (25) 26 27 28 29 30 > >>
			else {
				_start = currentPageNo - _gap > 0 ? currentPageNo - _gap : 1;
				_end = _start + maxDisplayedPages;
			}

			return new Boundaries { Start = _start, End = _end };
		}

		private TagBuilder CreatePagingLink(int targetPageNo, string text, string textSr, string pClass) {
			var liTag = new TagBuilder("li");
			liTag.AddCssClass("page-item");

			var aTag = new TagBuilder("a");
			aTag.AddCssClass("page-link");
			aTag.Attributes.Add("href", CreateUrlTemplate(targetPageNo, PageSize, QueryStringValue));

			if (string.IsNullOrWhiteSpace(textSr)) {
				aTag.InnerHtml.Append($"{targetPageNo}");
			} else {
				liTag.MergeAttribute("area-label", textSr);
				aTag.InnerHtml.AppendHtml($"<span area-hidden=\"true\">{text}</span>");
				aTag.InnerHtml.AppendHtml($"<span class=\"sr-only\">{textSr}</span>");
			}

			if (PageNo == targetPageNo) {
				liTag.AddCssClass($"{pClass}");
				aTag.Attributes.Add("tabindex", "-1");
				aTag.Attributes.Remove("href");
			}

			liTag.InnerHtml.AppendHtml(aTag);
			return liTag;
		}

		// Dropdown list for changing page size (items per page)
		private TagBuilder CreatePageSizeControl() {
			var dropDown = new TagBuilder("select");
			dropDown.AddCssClass($"form-control");
			dropDown.Attributes.Add("name", QueryStringKeyPageSize);
			dropDown.Attributes.Add("onchange", $"{PageSizeNavOnChange}");

			for (int i = 1; i <= PageSizeNavMaxItems; i++) {
				var option = new TagBuilder("option");
				option.InnerHtml.AppendHtml($"{i * PageSizeNavBlockSize}");

				if ((i * PageSizeNavBlockSize) == PageSize)
					option.Attributes.Add("selected", "selected");

				dropDown.InnerHtml.AppendHtml(option);
			}


			var fGroup = new TagBuilder("div");
			fGroup.AddCssClass("form-group");

			var label = new TagBuilder("label");
			label.Attributes.Add("for", "pageSizeControl");
			label.InnerHtml.AppendHtml($"{TextPageSize}&nbsp;");
			fGroup.InnerHtml.AppendHtml(label);
			fGroup.InnerHtml.AppendHtml(dropDown);

			var form = new TagBuilder("form");
			form.AddCssClass("form-inline");
			form.Attributes.Add("method", PageSizeNavFormMethod);
			form.InnerHtml.AppendHtml(fGroup);

			return form;
		}

		// Edit the url for each page, so it navigates to its target page number
		private string CreateUrlTemplate(int pageNo, int pageSize, string urlPath) {
			_logger.LogDebug($"----> Page No '{pageNo}', Page Size '{pageSize}', URL Path '{urlPath}'");

			string p = $"{QueryStringKeyPageNo}={pageNo}"; // CurrentPageNo query string parameter, Default: p
			string s = $"{QueryStringKeyPageSize}={pageSize}"; // PageSize query string parameter, Default: s

			var urlTemplate = urlPath.TrimStart('?').Split('&').ToList();

			for (int i = 0; i < urlTemplate.Count; i++) {
				var q = urlTemplate[i];
				urlTemplate[i] =
					q.StartsWith($"{QueryStringKeyPageNo}=") ? p :
					q.StartsWith($"{QueryStringKeyPageSize}=") ? s :
					q;
			}

			if (!urlTemplate.Any(x => x == p))
				urlTemplate.Add(p);

			if (!urlTemplate.Any(x => x == s))
				urlTemplate.Add(s);

			return "?" + string.Join("&", urlTemplate);
		}
	}
}
