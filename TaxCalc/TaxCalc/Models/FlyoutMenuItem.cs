using System;

namespace TaxCalc.Core.Models
{
    /// <summary>
    /// Information needed for a slide out menu item.
    /// </summary>
    public class FlyoutMenuItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public FlyoutMenuItem()
        {
            TargetType = typeof(FlyoutMenuItem);
        }

        /// <summary>
        /// The id of the menu item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the page.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The target page type to open.
        /// </summary>
        public Type TargetType { get; set; }
    }
}
