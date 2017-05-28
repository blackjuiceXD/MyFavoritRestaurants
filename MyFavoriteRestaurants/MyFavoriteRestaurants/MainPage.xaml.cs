using System;
using DLL;
using DLL.BE;
using DLL.BLL;
using DLL.Interface;
using Plugin.Media;
using Xamarin.Forms;


namespace MyFavoriteRestaurants
{
    public partial class MainPage : ContentPage
	{
		
        private IRespository<Restaurant> _restaurantRepository = new DLLFacade().GetRestaurantRepository();
        private IRespository<Food> _FoodRepository = new DLLFacade().GetFoodRepository();
        private MakeList MakeList = new MakeList();

	    private Restaurant restaurant;

        public MainPage()
        { 
            MakeList.FillDatabase();
            Title = "Restaurant";
            InitializeComponent();

            CrossMedia.Current.Initialize();
        }


        private async void Picked(object sender, EventArgs e)
        {
            if (ResList.SelectedItem != null)
            {
                restaurant = ResList.SelectedItem as Restaurant;
                await Navigation.PushAsync(new FoodPage(restaurant));
            }
           
        }

        private async void Options(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("choose Option","","","Add Restaurant","Details" ,"Delete Restaurant", "Cancel");

            switch (action)
            {
                case "Add Restaurant":
                    await Navigation.PushAsync(new CreateResPage());
                    break;
                case "Details":
                    if (ResList.SelectedItem != null)
                    {
                        restaurant = ResList.SelectedItem as Restaurant;
                        await Navigation.PushAsync(new ResDetailPage(restaurant));
                    }
                    break;
                case "Delete Restaurant":
                    if (ResList.SelectedItem != null)
                    {
                        restaurant = ResList.SelectedItem as Restaurant;
                        var answer = await DisplayAlert("Delete " + restaurant.Name, "Do you want to Delete " + restaurant.Name + "?", "Yes", "No");
                        if (answer == true)
                        {
                            foreach (var food in restaurant.Foods)
                            {
                                _FoodRepository.Delete(food.Id);
                            }
                            _restaurantRepository.Delete(restaurant.Id);
                            ResList.ItemsSource = _restaurantRepository.Read();
                        }
                    }
                    break;
                case "Cancel":
                    break;
            }
        }

	    protected override void OnAppearing()
	    {
	        ResList.ItemsSource = _restaurantRepository.Read();
	        base.OnAppearing();
	    }

    }
}

