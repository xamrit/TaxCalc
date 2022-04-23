using System;

namespace TaxCalc.Core.Models
{
    public class FlyoutMenuItem
    {
        public FlyoutMenuItem()
        {
            TargetType = typeof(FlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
