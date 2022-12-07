using System.Collections.ObjectModel;
using MovieApp.Helpers;
using MovieApp.MVVM.Model;
using MovieApp.Commands;
using System.Collections.Generic;
using System;

namespace MovieApp.MVVM.ViewModel
{
    public class MovieListingViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private MoviesModel<MovieItemModel> _response;
        public MoviesModel<MovieItemModel> Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MovieItemModel> _movies;
        public ObservableCollection<MovieItemModel> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        private MovieItemModel _selectedMovie = null;
        public MovieItemModel SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();

                HandleSelectedItem();
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

        private int _selectedGenre;
        public int SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                OnPropertyChanged();

                SearchValue = null;
                GetMovies();
            }
        }

        private List<int> _years;
        public List<int> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged();
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();

                SearchValue = null;
                GetMovies();
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

        private string _searchValue;
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                _searchValue = value;
                OnPropertyChanged();

                GetMovies();
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (value > TotalPages)
                {
                    value = TotalPages;
                }

                _currentPage = value > 0 ? value : 1;
                IsMaxReachedPage = value == TotalPages;

                OnPropertyChanged();
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged();
            }
        }

        private bool _isMaxReachedPage = false;
        public bool IsMaxReachedPage
        {
            get => _isMaxReachedPage;
            set
            {
                _isMaxReachedPage = value;
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

        public int PrevPageState { get; set; }

        public DecrementPageCommand PrevPage { get; set; }
        public IncrementPageCommand NextPage { get; set; }
        public ChangePageTextBoxCommand PageChange { get; set; }
        public GoBackToMoviesCommand GoBack { get; set; }

        public MovieListingViewModel()
        {
            GetGenreList();
            GetYearList();

            PrevPage = new DecrementPageCommand(this);
            NextPage = new IncrementPageCommand(this);
            PageChange = new ChangePageTextBoxCommand(this);
            GoBack = new GoBackToMoviesCommand(this);
        }

        public async void GetGenreList()
        {
            Genres = new List<GenreItemModel>();

            Requests req = new Requests();
            Genres = await req.FetchGenreList("genre/movie/list");
        }

        public void GetYearList()
        {
            Years = new List<int>();

            int currentYear = Convert.ToInt32(DateTime.Now.Year);

            for (int i = 1980; i <= currentYear; i++)
            {
                Years.Insert(0, i);
            }
        }

        public async void GetMovies(int page = 1)
        {
            try
            {
                IsLoading = true;

                Requests req = new Requests();

                if (SearchValue == "" || SearchValue == null)
                {
                    Response = SelectedGenre == 0
                        ? await req.FetchMovies("discover/movie", page, $"primary_release_year={SelectedYear}")
                        : await req.FetchMovies("discover/movie", page, $"primary_release_year={SelectedYear}&with_genres={SelectedGenre}");
                }
                else
                {
                    Response = await req.FetchMovies("search/movie", page, $"query={SearchValue}");
                }

                if (Response != null)
                {
                    PrevPageState = page;

                    Movies = new ObservableCollection<MovieItemModel>(Response.Results);
                    CurrentPage = Response.Page;
                    TotalPages = Response.Total_Pages > 500 ? 500 : Response.Total_Pages;

                    OnSortTypeChange();
                }
                else
                {
                    IsError = true;
                    CurrentPage = PrevPageState;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void HandleSelectedItem()
        {
            if (SelectedMovie != null)
            {
                CurrentView = new MovieDetailsViewModel(this, SelectedMovie.Id);
            }
        }

        public void OnSortTypeChange()
        {
            switch (SortType)
            {
                case "🠕 Title":
                    Movies = Sorting.SortByTitleAsc(Movies);
                    break;
                case "🠗 Title":
                    Movies = Sorting.SortByTitleDesc(Movies);
                    break;
                case "🠕 Rating":
                    Movies = Sorting.SortByRatingAsc(Movies);
                    break;
                case "🠗 Rating":
                    Movies = Sorting.SortByRatingDesc(Movies);
                    break;
                default:
                    break;
            }
        }

        public void OnDecrementPage()
        {
            if (CurrentPage > 1)
            {
                GetMovies(--CurrentPage);
            }
        }

        public void OnIncrementPage()
        {
            GetMovies(++CurrentPage);
        }

        public void OnTextBoxChange()
        {
            GetMovies(CurrentPage);
        }

        public void GoBackClick()
        {
            IsError = false;
            SearchValue = null;

            GetMovies(PrevPageState);
        }
    }
}