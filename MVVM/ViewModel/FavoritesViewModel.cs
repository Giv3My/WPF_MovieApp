using MovieApp.Commands;
using MovieApp.Helpers;
using MovieApp.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MovieApp.MVVM.ViewModel
{
    public class FavoritesViewModel : ViewModelBase
    {
        private ObservableCollection<FavoritesModel> _favoriteslist;
        public ObservableCollection<FavoritesModel> FavoritesList
        {
            get => _favoriteslist;
            set
            {
                _favoriteslist = value;
                OnPropertyChanged();
            }
        }

        private string _sortType;
        public string SortType
        {
            get => _sortType;
            set
            {
                _sortType = value;
                OnPropertyChanged();

                OnSortTypeChange();
            }
        }

        private bool _itemsExists;
        public bool ItemsExists
        {
            get => _itemsExists;
            set
            {
                _itemsExists = value;
                OnPropertyChanged();
            }
        }

        public RemoveFromFavoritesCommand RemoveFromFavorites { get; set; }

        public FavoritesViewModel()
        {
            LoadFavoriteList();

            ItemsExists = Convert.ToBoolean(FavoritesList.Count());

            RemoveFromFavorites = new RemoveFromFavoritesCommand(this);
        }

        public void LoadFavoriteList()
        {
            Files file = new Files("favorites.json");

            ObservableCollection<FavoritesModel> items = file.GetFileContent<ObservableCollection<FavoritesModel>>();

            FavoritesList = items != null ? new ObservableCollection<FavoritesModel>(items) : new ObservableCollection<FavoritesModel>();
        }

        public void UpdateFavoritesFile()
        {
            Files file = new Files("favorites.json");

            file.UpdateFile(FavoritesList);
        }

        public void OnRemoveClick(object id)
        {
            foreach (FavoritesModel item in FavoritesList)
            {
                if (item.Id == Convert.ToInt32(id))
                {
                    _ = FavoritesList.Remove(item);
                    break;
                }
            }

            ItemsExists = Convert.ToBoolean(FavoritesList.Count());

            UpdateFavoritesFile();
        }

        public void OnSortTypeChange()
        {
            switch (SortType)
            {
                case "Newest":
                    FavoritesList = Sorting.SortByTimeDesc(FavoritesList);
                    break;
                case "Oldest":
                    FavoritesList = Sorting.SortByTimeAsc(FavoritesList);
                    break;
                case "🠕 Title":
                    FavoritesList = Sorting.SortByTitleAsc(FavoritesList);
                    break;
                case "🠗 Title":
                    FavoritesList = Sorting.SortByTitleDesc(FavoritesList);
                    break;
                default:
                    break;
            }
        }
    }
}
