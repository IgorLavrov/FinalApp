using FinalApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace FinalApp.Views
{
    public class EditCompanyPage : ContentPage
    {
        private ListView _listView;
        private Entry _idEntry;
        private Entry _nameEntry;
        private Entry _addressEntry;
        private Entry _telephoneNumberEntry;
        private Entry _imageSource;
        private Button _button;
        Company _company=new Company();
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my08.db3");
        public EditCompanyPage()
        {
            this.Title = "Edit Company details";
            var db = new SQLiteConnection(_dbPath);
            StackLayout stackLayout = new StackLayout();
           

            _idEntry=new Entry();
            _idEntry.Placeholder = "ID";
            _idEntry.IsVisible = false;
            stackLayout.Children.Add(_idEntry);

            _nameEntry=new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Company Name";
           
            stackLayout.Children.Add(_nameEntry);
            _addressEntry=new Entry();
            _addressEntry.Keyboard = Keyboard.Text;
            _addressEntry.Placeholder = "Address";
           
            stackLayout.Children.Add(_addressEntry);
            _telephoneNumberEntry=new Entry();
            _telephoneNumberEntry.Keyboard = Keyboard.Text;
            _telephoneNumberEntry.Placeholder = "Telephone Number";
           
            stackLayout .Children.Add(_telephoneNumberEntry);
           
            _button = new Button
            {
                Text = "Update",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Yellow,
                TextColor = Color.Black,
                CornerRadius = 30,
                WidthRequest = 180,
                HeightRequest = 50

            };
            var absoluteLayout = new AbsoluteLayout();

            // Background Image
            var backgroundImage = new Image
            {
                Source = "wp.png", // replace with your image source
                Aspect = Aspect.AspectFill,
                Opacity = 0.15
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 1));
            absoluteLayout.Children.Add(backgroundImage);

            _button.Clicked += _button_Clicked;
            stackLayout.Children.Add(_button);

            Label label = new Label {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Margin = 10
            };
            label.Text = "List of Companies";
            stackLayout.Children.Add(label);

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Company>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
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
            Company company = new Company()
            {
                ID = Convert.ToInt32(_idEntry.Text),
                Name = _nameEntry.Text,
                Address = _addressEntry.Text,
                TelephoneNumber = _telephoneNumberEntry.Text
            };
            db.Update(company);
            await Navigation.PopAsync();
        }
        private void _listView_ItemSelected (object sender, SelectedItemChangedEventArgs e)
            {
            _company =(Company)e.SelectedItem;
            _idEntry.Text = _company.ID.ToString();
            _nameEntry.Text = _company.Name;
            _addressEntry.Text = _company.Address;
            _telephoneNumberEntry.Text = _company.TelephoneNumber;
        
        }
        
    }
}