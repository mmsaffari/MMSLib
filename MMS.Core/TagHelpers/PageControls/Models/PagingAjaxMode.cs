using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.PageControls.Models {
    /// <summary>
    /// ajax update modes
    /// </summary>
    public enum PagingAjaxMode {
        /// <summary>
        /// update before target content
        /// </summary>
        before,

        /// <summary>
        /// update after target content
        /// </summary>
        after,

        /// <summary>
        /// replace target content
        /// </summary>
        replace
    }
}
