using FinalApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;


namespace FinalApp.Views
{
    
    public class GetAllCompaniesPage : ContentPage
    {
        private ListView _listview;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my08.db3");
        public GetAllCompaniesPage()
        {
            this.Title = "Companies";

            var db = new SQLiteConnection(_dbPath);
            StackLayout stackLayout = new StackLayout();

            var absoluteLayout = new AbsoluteLayout();

            var backgroundImage = new Image
            {
                Source = "tree3.jpg", // replace with your image source
                Aspect = Aspect.AspectFill,
                Opacity = 0.15
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 1));
            absoluteLayout.Children.Add(backgroundImage);
            Label label = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Margin = 10,

            };
            label.Text = "List of Companies";
            stackLayout.Children.Add(label);

            _listview = new ListView();
            _listview.ItemsSource=db.Table<Company>().OrderBy(x => x.Name).ToList();
            //stackLayout.Children.Add(_listview);
            _listview.ItemSelected += OnItemSelected;

            stackLayout.Children.Add(_listview);

            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(0.5, 0.5, 1, 1));
            absoluteLayout.Children.Add(stackLayout
);
            // Set the AbsoluteLayout as the page content
            Content = absoluteLayout;
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedCompany = (Company)e.SelectedItem;

            await DisplayAlert("Company Details",
                $"Name: {selectedCompany.Name}\n" +
                $"Address: {selectedCompany.Address}\n" +
                $"Telephone Number: {selectedCompany.TelephoneNumber}\n",
               
                "OK");

            // Deselect the item
            _listview.SelectedItem = null;
        }
    }
}