using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Count30.ViewModels
{
    public class ViewInfoViewModel : ViewModelBase
    {
        public ViewInfoViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "情報";
        }
    }
}
