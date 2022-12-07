using MovieApp.Commands;
using MovieApp.Helpers;
using MovieApp.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MovieApp.MVVM.ViewModel
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        public MovieListingViewModel MovieListingVM { get; set; }

        private MovieDetailsModel _movieDetails;
        public MovieDetailsModel MovieDetails
        {
            get => _movieDetails;
            set
            {
                _movieDetails = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FavoritesModel> FavoritesList { get; set; }

        private bool _isInFavorites;
        public bool IsInFavorites
        {
            get => _isInFavorites;
            set
            {
                _isInFavorites = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isError = false;
        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                OnPropertyChanged();
            }
        }

        public NavigateToTrailerCommand OpenTrailer { get; set; }
        public GoBackToMovieListingCommand GoBack { get; set; }
        public ToggleFavoritesCommand ToggleFavorites { get; set; }

        public MovieDetailsViewModel() { }

        public MovieDetailsViewModel(MovieListingViewModel movieListingVM, int param)
        {
            MovieListingVM = movieListingVM;

            LoadFavoriteList();
            CheckInFavorites(param);
            GetMovieDetails(param);

            OpenTrailer = new NavigateToTrailerCommand(this);
            GoBack = new GoBackToMovieListingCommand(this);
            ToggleFavorites = new ToggleFavoritesCommand(this);
        }

        public async void GetMovieDetails(int id)
        {
            try
            {
                IsLoading = true;

                Requests req = new Requests();
                MovieDetails = await req.FetchMovieDetails("movie", id);

                IsError = MovieDetails == null;
            }
            finally
            {
                IsLoading = false;
            }
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

        public void OnTrailerBtnClick(object url)
        {
            _ = Process.Start(url.ToString());
        }

        public void OnFavoritesClick(object id)
        {
            if (!Convert.ToBoolean(FavoritesList.Count(x => x.Id == Convert.ToInt32(id))))
            {
                IsInFavorites = true;

                FavoritesList.Add(new FavoritesModel(MovieDetails));
            }
            else
            {
                IsInFavorites = false;

                foreach (FavoritesModel item in FavoritesList)
                {
                    if (item.Id == Convert.ToInt32(id))
                    {
                        _ = FavoritesList.Remove(item);
                        break;
                    }
                }
            }

            UpdateFavoritesFile();
        }

        public void CheckInFavorites(int id)
        {
            IsInFavorites = FavoritesList.Select(x => x.Id).Contains(id);
        }

        public void OnGoBackBtnClick()
        {
            MovieListingVM.SelectedMovie = null;
        }
    }
}
