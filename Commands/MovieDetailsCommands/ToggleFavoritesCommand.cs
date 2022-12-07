using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class ToggleFavoritesCommand : CommandBase
    {
        public MovieDetailsViewModel _movieDetailsViewModel;

        public ToggleFavoritesCommand(MovieDetailsViewModel viewModel)
        {
            _movieDetailsViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _movieDetailsViewModel.OnFavoritesClick(parameter);
        }
    }
}
