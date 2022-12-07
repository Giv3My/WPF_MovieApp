using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class GoBackToMoviesCommand : CommandBase
    {
        public MovieListingViewModel _movieListingViewModel;

        public GoBackToMoviesCommand(MovieListingViewModel viewModel)
        {
            _movieListingViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _movieListingViewModel.GoBackClick();
        }
    }
}
