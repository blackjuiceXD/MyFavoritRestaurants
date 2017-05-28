using System;
using DLL;
using DLL.BE;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFavoriteRestaurants
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResDetailPage : ContentPage
	{
	    private Restaurant _restaurant;
	    private PictureManager _pictureManager = new PictureManager();

		public ResDetailPage (Restaurant restaurant)
		{
		   
		    InitializeComponent();
		    _restaurant = restaurant;
		   

		}

	    private async void BtnEdit_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new ResEdit(_restaurant));
	    }

	    protected override void OnAppearing()
	    {
	        Title = _restaurant.Name;
            LblAddress.Text = _restaurant.Address;
            LblDescribe.Text = _restaurant.Describing;
            LblWebside.Text = _restaurant.Webside;
	        ResImage.Source =_pictureManager.GetResImage(_restaurant.ImagePath);
            base.OnAppearing();
	    }

	    
	}
}
