using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class ChangePageTextBoxCommand : CommandBase
    {
        public MovieListingViewModel _movieListingViewModel;

        public ChangePageTextBoxCommand(MovieListingViewModel viewModel)
        {
            _movieListingViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _movieListingViewModel.OnTextBoxChange();
        }
    }
}
