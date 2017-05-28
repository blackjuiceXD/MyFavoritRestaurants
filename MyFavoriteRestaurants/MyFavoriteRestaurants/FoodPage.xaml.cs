using System;
using DLL;
using DLL.BE;
using DLL.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFavoriteRestaurants
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodPage : ContentPage
	{
        private IRespository<Food> _foodRespository = new DLLFacade().GetFoodRepository();
        private IRespository<Restaurant> _restaurantRespository = new DLLFacade().GetRestaurantRepository();
	    private Restaurant _restaurant;

        public FoodPage (Restaurant restaurant)
		{
		    Title = restaurant.Name;
			InitializeComponent();
		    _restaurant = restaurant;
		}

	    private async void Details(object sender, EventArgs e)
	    {
	        if (FoodList.SelectedItem != null)
	        {
	           var food = FoodList.SelectedItem as Food;
	           await Navigation.PushAsync(new FoodDetailPage(food));
	        }
	    }

	    private async void Add(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CreateFood(_restaurant));
	    }

	    private async void Delete(object sender, EventArgs e)
	    {
	        if (FoodList.SelectedItem != null)
	        {
                var food = FoodList.SelectedItem as Food;
                var answer = await DisplayAlert("Delete " + food.Name,"Do you want to Delete " + food.Name + "?", "Yes", "No");

	            if (answer == true)
	            {
	                var restaurantId = food.RestaurantId;
	                _foodRespository.Delete(food.Id);
	                FoodList.ItemsSource = _restaurantRespository.Read(restaurantId).Foods;
	            }
            }
         }

	    protected override void OnAppearing()
	    {
	         
	        FoodList.ItemsSource = _restaurantRespository.Read(_restaurant.Id).Foods;
            base.OnAppearing();
	    }
	}
}
