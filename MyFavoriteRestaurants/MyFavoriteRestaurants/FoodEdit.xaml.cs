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
	public partial class FoodEdit : ContentPage
	{
	    private Food _food;
	    private string starRating;
	    private IRespository<Food> _foodRespository = new DLLFacade().GetFoodRepository();
        private PictureManager _pictureManager = new PictureManager();

		public FoodEdit (Food food)
		{
			InitializeComponent ();
            _food = food;
            FoodDescribe.Text = food.Describing;
		    FoodName.Text = food.Name;
		    FoodPrice.Text = food.Price.ToString();
		    FoodStarRating.Text = food.Rating + "/5";
		    CreateFoodImage.Source = _pictureManager.GetFoodImage(_food.ImagePath);
		    
		}

	    private async void BtnStarRating_OnClicked(object sender, EventArgs e)
	    {
            starRating = await DisplayActionSheet("Choose Rating", "", "", "1", "2", "3", "4", "5");

            FoodStarRating.Text = starRating + "/5";
        }

	    private  async void BtnTakePic_OnClicked(object sender, EventArgs e)
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

	        _food.ImagePath = file.Path;

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

            _food.ImagePath = file.Path;

            CreateFoodImage.Source = ImageSource.FromStream(() =>
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


	    private void BtnEditFood_OnClicked(object sender, EventArgs e)
	    {
	        _food.Name = FoodName.Text;
	        _food.Describing = FoodDescribe.Text;
	        _food.Price = Convert.ToSingle(FoodPrice.Text);
	        _food.Rating = Convert.ToInt32(starRating);
	        _foodRespository.Update(_food);
            Navigation.PopAsync();
	    }
	}
}
