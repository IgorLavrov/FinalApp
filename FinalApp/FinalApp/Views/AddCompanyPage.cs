using FinalApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Xamarin.Forms;

namespace FinalApp.Views
{
    public class AddCompanyPage : ContentPage
    {
        private Entry _nameEntry;
        private Entry _addressEntry;
        private Entry _TelephoneNumberEntry;
        private Entry _imageSource;
        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "my08.db3");
        public AddCompanyPage()
        {
            this.Title = "Add Company";
            StackLayout stackLayout = new StackLayout();

            var absoluteLayout = new AbsoluteLayout();

            // Background Image
            var backgroundImage = new Image
            {
                Source = "pic2.jpg", // replace with your image source
                Aspect = Aspect.AspectFill,
                Opacity = 0.15
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 1));
            absoluteLayout.Children.Add(backgroundImage);


            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Company name";
            stackLayout.Children.Add(_nameEntry);

            _addressEntry = new Entry();
            _addressEntry.Keyboard = Keyboard.Text;
            _addressEntry.Placeholder = "Address";
            stackLayout.Children.Add(_addressEntry);

            _TelephoneNumberEntry = new Entry();
            _TelephoneNumberEntry.Keyboard = Keyboard.Text;
            _TelephoneNumberEntry.Placeholder = "Phone";
            stackLayout.Children.Add(_TelephoneNumberEntry);

            _saveButton = new Button
            {
                Text = "Add",
                BackgroundColor = Color.Green,
                TextColor = Color.White,
                CornerRadius = 30,
                WidthRequest = 180,
                HeightRequest = 50
            };
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            stackLayout.VerticalOptions = LayoutOptions.CenterAndExpand;

            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(0.5, 0.5, 1, 1));
            absoluteLayout.Children.Add(stackLayout);

            // Set the AbsoluteLayout as the page content
            Content = absoluteLayout;
        }
        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Company>();

            string name = _nameEntry.Text;
            string address = _addressEntry.Text;
            string telephoneNumber = _TelephoneNumberEntry.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(address) && !string.IsNullOrWhiteSpace(telephoneNumber))
            {
                var maxPk = db.Table<Company>().OrderByDescending(c => c.ID).FirstOrDefault();

                Company company = new Company()
                {
                    ID = (maxPk == null ? 1 : maxPk.ID + 1),
                    Name = name,
                    Address = address,
                    TelephoneNumber = telephoneNumber
                };

                db.Insert(company);

                await DisplayAlert(null, company.Name + " Saved", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Please enter details", "OK");
            }

            await Navigation.PopAsync();
        }

        //private async void _saveButton_Clicked(object sender, EventArgs e)
        //{
        //    var db = new SQLiteConnection(_dbPath);
        //    db.CreateTable<Company>();
        //    var maxPk = db.Table<Company>().OrderByDescending(c => c.ID).FirstOrDefault();

        //    Company company = new Company()
        //    {
        //        ID = (maxPk == null ? 1 : maxPk.ID + 1),
        //        Name = _nameEntry.Text,
        //        Address = _addressEntry.Text,
        //        TelephoneNumber = _TelephoneNumberEntry.Text

        //    };


        //    db.Insert( company );

        //    await DisplayAlert(null, company.Name + " Saved", "Ok");

        //    await Navigation.PopAsync();
    //}
    }
}
