using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieApp.Commands
{
    public class DecrementPageCommand : CommandBase
    {
        public MovieListingViewModel _movieListingViewModel;

        public DecrementPageCommand(MovieListingViewModel viewModel)
        {
            _movieListingViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _movieListingViewModel.OnDecrementPage();
        }
    }
}
