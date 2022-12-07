using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MVVM.Model
{
    public class GenresModel
    {
        public List<GenreItemModel> Genres { get; set; }
    }

    public class GenreItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
