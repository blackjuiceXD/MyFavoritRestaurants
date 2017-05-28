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
	public partial class CreateResPage : ContentPage
	{
	    private IRespository<Restaurant> _restaurantRespository = new DLLFacade().GetRestaurantRepository();
	    private string imagePath;


        public CreateResPage ()
		{
		    Title = "Add a new Restaurants";
			InitializeComponent();
		    CreateImage.Source = "kokkehue.jpg";
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

            imagePath = file.Path;

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


	         imagePath = file.Path;

            CreateImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }


        private async void BtnAddRestaurant_OnClicked(object sender, EventArgs e)
	    {
	        if (ResName.Text != null)
	        {
	            Restaurant restaurant = new Restaurant();
	            restaurant.Name = ResName.Text;
	            restaurant.Address = ResAddress.Text;
	            restaurant.Describing = ResDescribe.Text;
	            restaurant.Webside = ResWebside.Text;
	            restaurant.ImagePath = imagePath;
	            _restaurantRespository.Create(restaurant);
	            await Navigation.PopAsync();
	        }
	        else
	        {
	            await DisplayAlert("No Name", "Please Enter a Name", "Ok");
	        }
	    }

	    private void BtnCancel_OnClicked(object sender, EventArgs e)
	    {
            Navigation.PopAsync();
        }
	}
}
