using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Count30.ViewModels
{
    public class ViewGameViewModel : ViewModelBase
    {
        public ViewGameViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "ゲーム";
        }
    }
}
