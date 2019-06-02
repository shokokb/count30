using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Count30.Models;
using Count30.Views;

namespace Count30.ViewModels
{
    public class MenuPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        private ImageSource _hamburgerIcon;

        public ObservableCollection<MyMenuItem> MenuItems { get; set; }

        public ImageSource  HamburgerIcon
        {
            get => _hamburgerIcon;
            set => SetProperty(ref _hamburgerIcon, value);
        }

        private MyMenuItem selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            HamburgerIcon = ImageSource.FromResource("Count30.Image.ic_hamburger.png");

            MenuItems = new ObservableCollection<MyMenuItem>();

            MenuItems.Add(new MyMenuItem()
            {
                Icon = ImageSource.FromResource("Count30.Image.ic_view_game.png"),
                PageName = nameof(ViewGame),
                Title = "ゲーム"
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = ImageSource.FromResource("Count30.Image.ic_view_info.png"),
                PageName = nameof(ViewInfo),
                Title = "情報"
            });

            NavigateCommand = new DelegateCommand(Navigate);
        }

        async void Navigate()
        {
            await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + SelectedMenuItem.PageName);
        }
    }
}