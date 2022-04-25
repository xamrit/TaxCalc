using System;
using TaxCalc.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxCalc.Core.Views
{
    /// <summary>
    /// The main flyout page of the app.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainFlyoutPage : FlyoutPage
    {
        public MainFlyoutPage()
        {
            InitializeComponent();
            Detail = new BaseNavigationPage(new TaxRatePage());

            SlideoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        /// <summary>
        /// Defines what occurs on slide out menu taps. Opens the corresponding page with
        /// the menu item.
        /// </summary>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new BaseNavigationPage(page);
            IsPresented = false;

            SlideoutPage.ListView.SelectedItem = null;
        }
    }
}