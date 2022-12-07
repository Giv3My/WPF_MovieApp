using MovieApp.Commands;
using MovieApp.Helpers;
using MovieApp.MVVM.Model;
using System;
using System.Collections.Generic;

namespace MovieApp.MVVM.ViewModel
{
    public class RandomMovieViewModel : ViewModelBase
    {
        private MovieDetailsModel _randomMovie;
        public MovieDetailsModel RandomMovie
        {
            get => _randomMovie;
            set
            {
                _randomMovie = value;
                OnPropertyChanged();
            }
        }

        private List<GenreItemModel> _genres;
        public List<GenreItemModel> Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged();
            }
        }

        private List<int> _yearsFrom;
        public List<int> YearsFrom
        {
            get => _yearsFrom;
            set
            {
                _yearsFrom = value;
                OnPropertyChanged();
            }
        }

        private List<int> _yearsTo;
        public List<int> YearsTo
        {
            get => _yearsTo;
            set
            {
                _yearsTo = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGenre;
        public int SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                OnPropertyChanged();
            }
        }

        private int _selectedYearFrom;
        public int SelectedYearFrom
        {
            get => _selectedYearFrom;
            set
            {
                _selectedYearFrom = value;
                OnPropertyChanged();
            }
        }

        private int _selectedYearTo;
        public int SelectedYearTo
        {
            get => _selectedYearTo;
            set
            {
                _selectedYearTo = value;
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

        private bool _isNoAction = true;
        public bool IsNoAction
        {
            get => _isNoAction;
            set
            {
                _isNoAction = value;
                OnPropertyChanged();
            }
        }

        public RandomMovieClickCommand RandomMovieClick { get; set; }

        public RandomMovieViewModel()
        {
            GetGenreList();
            GetYearList();

            RandomMovieClick = new RandomMovieClickCommand(this);
        }

        public async void GetGenreList()
        {
            Genres = new List<GenreItemModel>();

            Requests req = new Requests();
            Genres = await req.FetchGenreList("genre/movie/list");
        }

        public void GetYearList()
        {
            YearsFrom = new List<int>();
            YearsTo = new List<int>();

            int currentYear = Convert.ToInt32(DateTime.Now.Year);

            for (int i = 1980; i <= currentYear; i++)
            {
                YearsFrom.Add(i);
                YearsTo.Insert(0, i);
            }
        }

        public async void GetRandomMovie()
        {
            try
            {
                IsNoAction = false;
                IsLoading = true;
                IsError = false;

                Requests req = new Requests();

                string dateFrom = new DateTime(SelectedYearFrom, 1, 1).ToString("yyyy-MM-dd");
                string dateTo = new DateTime(SelectedYearTo, 12, 31).ToString("yyyy-MM-dd");

                RandomMovie = SelectedGenre == 0
                            ? await req.FetchRandomMovie("discover/movie", dateFrom, dateTo)
                            : await req.FetchRandomMovie("discover/movie", dateFrom, dateTo, $"with_genres={SelectedGenre}");

                if (RandomMovie == null)
                {
                    IsError = true;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
