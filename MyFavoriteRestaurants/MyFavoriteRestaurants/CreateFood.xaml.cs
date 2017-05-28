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
	public partial class CreateFood : ContentPage
	{
	    private IRespository<Food> _foodRespository = new DLLFacade().GetFoodRepository();
	    private IRespository<Restaurant> _restaurantRespository = new DLLFacade().GetRestaurantRepository();
	    private Restaurant _restaurant;
	    private string starRating;
	    private string imagePath;
        private PictureManager _pictureManager = new PictureManager();

		public CreateFood (Restaurant restaurant)
		{
		    Title = "Add Food";
			InitializeComponent();
		    FoodStarRating.Text = "0/5";
		    CreateFoodImage.Source = "Kniv_og_gaffelx12x1.jpg";
		    _restaurant = restaurant;
		}

	    private async void BtnStarRating_OnClicked(object sender, EventArgs e)
	    {
            starRating = await DisplayActionSheet("Choose Rating", "", "", "1", "2", "3", "4","5");

	        FoodStarRating.Text = starRating + "/5";
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

            CreateFoodImage.Source = ImageSource.FromStream(() =>
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

            CreateFoodImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

	    private async void BtnAddFood_OnClicked(object sender, EventArgs e)
	    {
	        if (FoodName.Text != null && starRating != null && FoodPrice.Text != null)
	        {
	            Food food = new Food();
	            food.Name = FoodName.Text;
	            food.Price = Convert.ToSingle(FoodPrice.Text);
	            food.Describing = FoodDescribe.Text;
	            food.Rating = Convert.ToInt32(starRating);
	            food.ImagePath = imagePath;
	            food.RestaurantId = _restaurant.Id;
	            _foodRespository.Create(food);
                _restaurant.Foods.Add(food);
	            _restaurantRespository.Update(_restaurant);
	            await Navigation.PopAsync();
	        }
	        else
	        {
                await DisplayAlert("No Name or no Rating or No Price", "Please Enter a Name, a Price and give a Rating", "Ok");
            }
	    }

	    private void BtnCancel_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PopAsync();
	    }
	}
}
