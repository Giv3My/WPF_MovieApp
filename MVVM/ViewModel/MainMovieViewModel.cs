using MovieApp.Helpers;
using MovieApp.MVVM.Model;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MovieApp.MVVM.ViewModel
{
    public class MainMovieViewModel : ViewModelBase
    {
        public MainMovieViewModel()
        {
            CreateFavoritesFile();
        }

        public void CreateFavoritesFile()
        {
            Files file = new Files("favorites.json");

            file.CreateFile();
        }
    }
}
