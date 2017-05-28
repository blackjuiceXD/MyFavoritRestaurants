using Xamarin.Forms;

namespace DLL
{
    public class PictureManager
    {
        //sets imageSourse by string
        //if the string is  empty it set a imageSource to a default image.
        //this one is for Restaurants.
        public ImageSource GetResImage(string imagePath)
        {
            if (imagePath != null)
            {
                return ImageSource.FromFile(imagePath);
            }
            else
            {
                return "kokkehue.jpg";
            }
        }

        //sets imageSourse by string
        //if the string is  empty it set a imageSource to a default image.
        //this one is for ¨food.
        public ImageSource GetFoodImage(string imagePath)
        {
            if (imagePath != null)
            {
                return ImageSource.FromFile(imagePath);
            }
            else
            {
                return "Kniv_og_gaffelx12x1.jpg";
            }
        }
    }
}
