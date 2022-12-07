using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MVVM.Model
{
    public class ActorsModel<T>
    {
        public ObservableCollection<T> Cast { get; set; }
    }

    public class ActorItemModel
    {
        public string Original_Name { get; set; }

        private string _profile_path;
        public string Profile_Path
        {
            get => _profile_path;
            set => _profile_path = value != null ? $"https://image.tmdb.org/t/p/w780{value}" : null;
        }
    }
}
