using System;
using DLL;
using DLL.BE;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFavoriteRestaurants
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDetailPage : ContentPage
	{
        private Food _food;
        private PictureManager _pictureManager = new PictureManager();

        public FoodDetailPage (Food food)
		{
		    Title = food.Name;
			InitializeComponent ();
		    _food = food;
		}

	    private async void BtnEdit_OnClicked(object sender, EventArgs e)
	    {
	       await Navigation.PushAsync(new FoodEdit(_food));
	    }

        //sets images of stars by rating
	    private void SetStarRating(Food food)
	    {
	        switch (food.Rating)
	        {
                case 1:
	                StarImage1.Source = "fuldstjerne.png";
                    StarImage2.Source = "tomstjerne.png";
                    StarImage3.Source = "tomstjerne.png";
                    StarImage4.Source = "tomstjerne.png";
                    StarImage5.Source = "tomstjerne.png";
                    break;
                case 2:
                    StarImage1.Source = "fuldstjerne.png";
                    StarImage2.Source = "fuldstjerne.png";
                    StarImage3.Source = "tomstjerne.png";
                    StarImage4.Source = "tomstjerne.png";
                    StarImage5.Source = "tomstjerne.png";
                    break;
                case 3:
                    StarImage1.Source = "fuldstjerne.png";
                    StarImage2.Source = "fuldstjerne.png";
                    StarImage3.Source = "fuldstjerne.png";
                    StarImage4.Source = "tomstjerne.png";
                    StarImage5.Source = "tomstjerne.png";
                    break;
                case 4:
                    StarImage1.Source = "fuldstjerne.png";
                    StarImage2.Source = "fuldstjerne.png";
                    StarImage3.Source = "fuldstjerne.png";
                    StarImage4.Source = "fuldstjerne.png";
                    StarImage5.Source = "tomstjerne.png";
                    break;
                case 5:
                    StarImage1.Source = "fuldstjerne.png";
                    StarImage2.Source = "fuldstjerne.png";
                    StarImage3.Source = "fuldstjerne.png";
                    StarImage4.Source = "fuldstjerne.png";
                    StarImage5.Source = "fuldstjerne.png";
                    break;
            }
	    }

	    protected override void OnAppearing()
	    {
            FoodImage.Source = _pictureManager.GetFoodImage(_food.ImagePath);
            LblDescribe.Text = _food.Describing;
            LblPrice.Text = _food.Price.ToString();
            SetStarRating(_food);
            base.OnAppearing();
	    }
	}
}
