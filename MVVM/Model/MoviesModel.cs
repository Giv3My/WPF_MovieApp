using System.Collections.ObjectModel;

namespace MovieApp.MVVM.Model
{
    public class MoviesModel<T>
    {
        public int Page { get; set; }

        public int Total_Pages { get; set; }

        public ObservableCollection<T> Results { get; set; }
    }

    public class MovieItemModel
    {
        public int Id { get; set; }

        private string _poster_path;
        public string Poster_Path
        {
            get => _poster_path;
            set => _poster_path = value != null ? $"https://image.tmdb.org/t/p/w780{value}" : null;
        }

        public string Title { get; set; }

        public float Vote_Average { get; set; }
    }
}
