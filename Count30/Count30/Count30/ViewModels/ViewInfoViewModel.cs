using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Count30.ViewModels
{
    public class ViewInfoViewModel : ViewModelBase
    {
        private string _twitterId = "@shoko_kb";
        private string _url = "https://shoko.mitose.jp/";
        private string _twitterUrl = $"https://twitter.com/shoko_kb";

        public ICommand AccessWebSiteCommand { get; }
        public ICommand AccessTwitterCommand { get; }

        public string TwitterId 
        {
            get => _twitterId;
            set => SetProperty(ref _twitterId, value);
        }
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public string TwitterUrl
        {
            get => _twitterUrl;
            set => SetProperty(ref _twitterUrl, value);
        }

        public ViewInfoViewModel(
            INavigationService navigationService, 
            Prism.Services.IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "情報";


            AccessWebSiteCommand = new DelegateCommand(() =>
            {
                Device.OpenUri(new Uri(Url));
            });

            AccessTwitterCommand = new DelegateCommand(() =>
            {
                Device.OpenUri(new Uri(TwitterUrl));
            });
        }
    }
}
