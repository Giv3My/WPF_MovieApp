using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class RandomMovieClickCommand : CommandBase
    {
        public RandomMovieViewModel _randomMovieViewModel;

        public RandomMovieClickCommand(RandomMovieViewModel viewModel)
        {
            _randomMovieViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _randomMovieViewModel.GetRandomMovie();
        }
    }
}
