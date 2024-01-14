using FinalApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            _listview.ItemsSource = db.Table<Company>().OrderBy(x => x.Name).ToList();
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

        //private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem == null)
        //        return;

        //    var selectedCompany = (Company)e.SelectedItem;

        //    var result = await DisplayAlert("Company Details",
        //        $"Name: {selectedCompany.Name}\n" +
        //        $"Address: {selectedCompany.Address}\n" +
        //        $"Telephone Number: {selectedCompany.TelephoneNumber}\n",
        //        "Call", "OK");

        //    // Deselect the item
        //    _listview.SelectedItem = null;

        //    if (result)
        //    {
        //        // User clicked "Call"
        //        if (!string.IsNullOrWhiteSpace(selectedCompany.TelephoneNumber))
        //        {
        //            // Check if the telephone number is not empty
        //            try
        //            {
        //                Device.OpenUri(new Uri(String.Format("tel:{0}", selectedCompany.TelephoneNumber)));
        //                //PhoneDialer.Open(selectedCompany.TelephoneNumber);
        //            }
        //            catch (FeatureNotSupportedException)
        //            {
        //                await DisplayAlert("Error", "Phone dialing is not supported on this device.", "OK");
        //            }
        //            catch (Exception ex)
        //            {
        //                await DisplayAlert("Error", $"Failed to initiate the call. {ex.Message}", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Telephone number is not provided.", "OK");
        //        }
        //    }
        //}
        //private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem == null)
        //        return;

        //    var selectedCompany = (Company)e.SelectedItem;

        //    var result = await DisplayAlert("Company Details",
        //        $"Name: {selectedCompany.Name}\n" +
        //        $"Address: {selectedCompany.Address}\n" +
        //        $"Telephone Number: {selectedCompany.TelephoneNumber}\n",
        //        "Map", "Call");

        //    // Deselect the item
        //    _listview.SelectedItem = null;



        //    if (result)
        //    {
        //        // User clicked "Map"
        //        if (!string.IsNullOrWhiteSpace(selectedCompany.Address))
        //        {
        //            try
        //            {
        //                var location = await Geocoding.GetLocationsAsync(selectedCompany.Address);

        //                if (location != null && location.Any())
        //                {
        //                    var firstLocation = location.First();
        //                    var options = new MapLaunchOptions { Name = selectedCompany.Name };

        //                    await Map.OpenAsync(firstLocation, options);
        //                }
        //                else
        //                {
        //                    await DisplayAlert("Error", "Unable to find the location on the map.", "OK");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                await DisplayAlert("Error", $"Failed to find the location on the map. {ex.Message}", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Address is not provided.", "OK");
        //        }
        //    }
        //    else if (result == false)
        //    {
        //        // User clicked "Call"
        //        if (!string.IsNullOrWhiteSpace(selectedCompany.TelephoneNumber))
        //        {
        //            // Check if the telephone number is not empty
        //            try
        //            {
        //                 Device.OpenUri(new Uri(String.Format("tel:{0}", selectedCompany.TelephoneNumber)));
        //            }
        //            catch (FeatureNotSupportedException)
        //            {
        //                await DisplayAlert("Error", "Phone dialing is not supported on this device.", "OK");
        //            }
        //            catch (Exception ex)
        //            {
        //                await DisplayAlert("Error", $"Failed to initiate the call. {ex.Message}", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Telephone number is not provided.", "OK");
        //        }
        //    }
        //}

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedCompany = (Company)e.SelectedItem;

            var message = 
                          $"Address: {selectedCompany.Address}\n" +
                          $"Telephone Number: {selectedCompany.TelephoneNumber}";

            // Create buttons
            var mapButton = new Button { Text = "Map", Command = new Command(async () => await HandleMapAction(selectedCompany)) };
            var callButton = new Button { Text = "Call", Command = new Command(() => HandleCallAction(selectedCompany)) };
            var cancelButton = new Button { Text = "Cancel" };

            // StackLayout to hold buttons horizontally
            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { mapButton, callButton, cancelButton }
            };

            // StackLayout for the entire content (message + buttons)
            var contentStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children = { new Label { Text = message }, buttonStack }
            };

            var action = await DisplayActionSheet(message,null, "Cancel", "Map" , "Call");

            // Deselect the item
            _listview.SelectedItem = null;

            switch (action)
            {
                case "Map":
                    await HandleMapAction(selectedCompany);
                    break;
                case "Call":
                    HandleCallAction(selectedCompany);
                    break;
            }
        }

        private async Task HandleMapAction(Company selectedCompany)
        {
            // User selected "Map"
            if (!string.IsNullOrWhiteSpace(selectedCompany.Address))
            {
                try
                {
                    var location = await Geocoding.GetLocationsAsync(selectedCompany.Address);

                    if (location != null && location.Any())
                    {
                        var firstLocation = location.First();
                        var options = new MapLaunchOptions { Name = selectedCompany.Name };

                        await Map.OpenAsync(firstLocation, options);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Unable to find the location on the map.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to find the location on the map. {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Address is not provided.", "OK");
            }
        }

        private void HandleCallAction(Company selectedCompany)
        {
            // User selected "Call"
            if (!string.IsNullOrWhiteSpace(selectedCompany.TelephoneNumber))
            {
                try
                {
                    Device.OpenUri(new Uri(String.Format("tel:{0}", selectedCompany.TelephoneNumber)));
                }
                catch (FeatureNotSupportedException)
                {
                    DisplayAlert("Error", "Phone dialing is not supported on this device.", "OK");
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", $"Failed to initiate the call. {ex.Message}", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Telephone number is not provided.", "OK");
            }
        }


    }
}