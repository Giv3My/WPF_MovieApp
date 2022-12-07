using MovieApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Commands
{
    public class RemoveFromFavoritesCommand : CommandBase
    {
        public FavoritesViewModel _favoritesViewModel;

        public RemoveFromFavoritesCommand(FavoritesViewModel viewModel)
        {
            _favoritesViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _favoritesViewModel.OnRemoveClick(parameter);
        }
    }
}
