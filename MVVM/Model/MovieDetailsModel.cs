using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MovieApp.MVVM.Model
{
    public class MovieDetailsModel
    {
        public int Id { get; set; }

        public string Original_Title { get; set; }

        public string Overview { get; set; }

        public string TrailerLink { get; set; }

        public ObservableCollection<ActorItemModel> Actors { get; set; }

        public VideosModel Videos
        {
            set => TrailerLink = value.Results.Count() > 0 ? value.Results[0].Key : null;
        }

        private string _backdrop_path;
        public string Backdrop_Path
        {
            get => _backdrop_path;
            set => _backdrop_path = value != null ? $"https://image.tmdb.org/t/p/w1280{value}" : null;
        }

        private string _poster_path;
        public string Poster_Path
        {
            get => _poster_path;
            set => _poster_path = value != null ? $"https://image.tmdb.org/t/p/w780{value}" : null;
        }

        public string Country { get; set; }
        public List<CountryItemModel> Production_Countries
        {
            set => Country = value.Count() > 0 ? string.Join(", ", value.Select(item => item.Name)) : null;
        }

        public string GenreStr { get; set; }
        public List<GenreItemModel> Genres
        {
            set => GenreStr = value.Count() > 0 ? string.Join(", ", value.Select(item => item.Name)) : null;
        }

        public string Year { get; set; }
        private string _release_date;
        public string Release_Date
        {
            get => _release_date;
            set
            {
                Year = value.Split('-')[0];
                _release_date = Convert.ToDateTime(value).ToString("MMMM dd, yyyy");
            }
        }

        private string _runtime;
        public string Runtime
        {
            get => _runtime;
            set => _runtime = value != null ? $"{Math.Floor(Convert.ToDouble(Convert.ToInt32(value) / 60))}h. {Convert.ToInt32(value) % 60}m." : null;
        }

        private string _tagline;
        public string Tagline
        {
            get => _tagline;
            set => _tagline = value == "" || value == null ? null : value;
        }
    }

    public class CountryItemModel
    {
        public string Name { get; set; }
    }
}
