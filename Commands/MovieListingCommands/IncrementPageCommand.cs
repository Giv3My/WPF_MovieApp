using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class IncrementPageCommand : CommandBase
    {
        public MovieListingViewModel _movieListingViewModel;

        public IncrementPageCommand(MovieListingViewModel viewModel)
        {
            _movieListingViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _movieListingViewModel.OnIncrementPage();
        }
    }
}
