using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MVVM.Model
{
    public class VideosModel
    {
        public List<VideoItemModel> Results { get; set; }
    }

    public class VideoItemModel
    {
        private string _key;
        public string Key
        {
            get => _key;
            set
            {
                _key = $"https://www.youtube.com/watch?v={value}";
            }
        }
    }
}
