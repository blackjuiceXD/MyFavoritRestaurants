using System;
using DLL;
using DLL.BE;
using DLL.Interface;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFavoriteRestaurants
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResEdit : ContentPage
	{
	    private Restaurant _restaurant;
	    private IRespository<Restaurant> _restaurantRespository = new DLLFacade().GetRestaurantRepository();
        private PictureManager _pictureManager = new PictureManager();

        public ResEdit (Restaurant restaurant)
		{
			InitializeComponent ();
		    ResAddress.Text = restaurant.Address;
		    ResDescribe.Text = restaurant.Describing;
		    ResName.Text = restaurant.Name;
		    ResWebside.Text = restaurant.Webside;
		    _restaurant = restaurant;
		   CreateImage.Source = _pictureManager.GetResImage(_restaurant.ImagePath);
		}

	    private async void BtnTakePic_OnClicked(object sender, EventArgs e)
	    {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;

            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "sample",
                Name = "test.jpg",
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 8
            });

            if (file == null)
            {
                await DisplayAlert("No file", "No file created.", "OK");
                return;
            }

	        _restaurant.ImagePath = file.Path;

            CreateImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

	    private async void BtnChoosePic_OnClicked(object sender, EventArgs e)
	    {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 8
            });

            if (file == null)
                return;

            _restaurant.ImagePath = file.Path;

            CreateImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

	    private void BtnCancel_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PopAsync();
	    }

	    private void BtnEditRestaurant_OnClicked(object sender, EventArgs e)
	    {
	        _restaurant.Address = ResAddress.Text;
	        _restaurant.Describing = ResDescribe.Text;
	        _restaurant.Name = ResName.Text;
	        _restaurant.Webside = ResWebside.Text;
            _restaurantRespository.Update(_restaurant);
            Navigation.PopAsync();
	    }
	}
}
