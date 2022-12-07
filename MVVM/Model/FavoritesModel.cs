using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MVVM.Model
{
    public class FavoritesModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Poster_Path { get; set; }

        public DateTime Added_At { get; set; }

        public FavoritesModel() { }

        public FavoritesModel(MovieDetailsModel model)
        {
            Id = model.Id;
            Title = model.Original_Title;
            Poster_Path = model.Poster_Path;
            Added_At = DateTime.Now;
        }
    }
}
