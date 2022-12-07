using MovieApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Helpers
{
    public class Sorting
    {
        public Sorting() { }

        public static ObservableCollection<MovieItemModel> SortByTitleAsc(ObservableCollection<MovieItemModel> list)
        {
            return new ObservableCollection<MovieItemModel>(list.OrderBy(item => item.Title));
        }

        public static ObservableCollection<MovieItemModel> SortByTitleDesc(ObservableCollection<MovieItemModel> list)
        {
            return new ObservableCollection<MovieItemModel>(list.OrderByDescending(item => item.Title));
        }

        public static ObservableCollection<MovieItemModel> SortByRatingAsc(ObservableCollection<MovieItemModel> list)
        {
            return new ObservableCollection<MovieItemModel>(list.OrderBy(item => item.Vote_Average));
        }
        public static ObservableCollection<MovieItemModel> SortByRatingDesc(ObservableCollection<MovieItemModel> list)
        {
            return new ObservableCollection<MovieItemModel>(list.OrderByDescending(item => item.Vote_Average));
        }

        public static ObservableCollection<FavoritesModel> SortByTitleAsc(ObservableCollection<FavoritesModel> list)
        {
            return new ObservableCollection<FavoritesModel>(list.OrderBy(item => item.Title));
        }

        public static ObservableCollection<FavoritesModel> SortByTitleDesc(ObservableCollection<FavoritesModel> list)
        {
            return new ObservableCollection<FavoritesModel>(list.OrderByDescending(item => item.Title));
        }

        public static ObservableCollection<FavoritesModel> SortByTimeAsc(ObservableCollection<FavoritesModel> list)
        {
            return new ObservableCollection<FavoritesModel>(list.OrderBy(item => item.Added_At));
        }

        public static ObservableCollection<FavoritesModel> SortByTimeDesc(ObservableCollection<FavoritesModel> list)
        {
            return new ObservableCollection<FavoritesModel>(list.OrderByDescending(item => item.Added_At));
        }
    }
}
