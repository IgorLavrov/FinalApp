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
    public class DeleteCompanyPage : ContentPage
    {
        private ListView _listView;
        private Button _button;
        Company _company= new Company();
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my08.db3");
        public DeleteCompanyPage()
        {


            this.Title = "Edit Company details";
            var db = new SQLiteConnection(_dbPath);
            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Company>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;

            var absoluteLayout = new AbsoluteLayout();

            // Background Image
            var backgroundImage = new Image
            {
                Source = "pic4.jpg", // replace with your image source
                Aspect = Aspect.AspectFill,
                Opacity = 0.15
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 1));
            absoluteLayout.Children.Add(backgroundImage);

            _button = new Button
            {
                Text ="Delete",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                CornerRadius = 30,
                WidthRequest = 180,
                HeightRequest = 50
            };
            _button.Clicked += _button_Clicked;
    
            stackLayout.Children.Add(_button);
            stackLayout.Children.Add(_listView);


            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(0.5, 0.5, 1, 1));
            absoluteLayout.Children.Add(stackLayout);

            // Set the AbsoluteLayout as the page content
            Content = absoluteLayout;
        }

        private async void _button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            if (_company != null && _company.ID != 0)
            {

                db.Table<Company>().Delete(x => x.ID == _company.ID
                );

            }
            else
            {
                await DisplayAlert("Error", "Please select a company to delete", "OK");
            }
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _company = (Company)e.SelectedItem;
            

        }



    }
}