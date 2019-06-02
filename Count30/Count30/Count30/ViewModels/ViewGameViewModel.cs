using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Count30.ViewModels
{
    public class ViewGameViewModel : ViewModelBase
    {
        const int SEC = 1;
        const int MAX = 30;

        public enum E_Player
        {
            Man,
            Computer
        }

        private E_Player _player = E_Player.Man;
        private string _turn = "";

        private ImageSource _icon;

        private int _curNum = 0;
        private int _leftNum = 1;
        private int _centerNum = 2;
        private int _rightNum = 3;

        private bool _isEnabledLeft = false;
        private bool _isEnabledCenter = false;
        private bool _isEnabledRight = false;

        private bool _isEnabledStart = true;
        private bool _isEnabledSwitch = false;
        private bool _isEnabledQuit = false;

        public E_Player Player
        {
            get => _player;
            set => SetProperty(ref _player, value);
        }

        public string Turn
        {
            get => _turn;
            set => SetProperty(ref _turn, value);
        }

        public ImageSource Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public int CurNum
        {
            get => _curNum;
            set => SetProperty(ref _curNum, value);
        }

        public int LeftNum
        {
            get => _leftNum;
            set => SetProperty(ref _leftNum, value);
        }

        public int CenterNum
        {
            get => _centerNum;
            set => SetProperty(ref _centerNum, value);
        }

        public int RightNum
        {
            get => _rightNum;
            set => SetProperty(ref _rightNum, value);
        }

        public bool IsEnabledLeft
        {
            get => _isEnabledLeft;
            set => SetProperty(ref _isEnabledLeft, value);
        }

        public bool IsEnabledCenter
        {
            get => _isEnabledCenter;
            set => SetProperty(ref _isEnabledCenter, value);
        }

        public bool IsEnabledRight
        {
            get => _isEnabledRight;
            set => SetProperty(ref _isEnabledRight, value);
        }

        public bool IsEnabledStart
        {
            get => _isEnabledStart;
            set => SetProperty(ref _isEnabledStart, value);
        }

        public bool IsEnabledSwitch
        {
            get => _isEnabledSwitch;
            set => SetProperty(ref _isEnabledSwitch, value);
        }

        public bool IsEnabledQuit
        {
            get => _isEnabledQuit;
            set => SetProperty(ref _isEnabledQuit, value);
        }

        public ICommand LeftCommand { get; }
        public ICommand CenterCommand { get; }
        public ICommand RightCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand SwitchCommand { get; }
        public ICommand QuitCommand { get; }

        public 
            ViewGameViewModel(
            INavigationService navigationService,
            Prism.Services.IPageDialogService dialogService) 
            : base(navigationService, dialogService)
        {
            Title = "ゲーム";

            Icon = ImageSource.FromResource("Count30.Image.ic_count_30_400.png");

            LeftCommand = new DelegateCommand(() =>
            {
                CurNum = LeftNum;
                IsEnabledLeft = false;
                IsEnabledCenter = true;
                IsEnabledRight = false;
                IsEnabledStart = false;
                IsEnabledSwitch = true;
                IsEnabledQuit = true;
                Judge();
            });

            CenterCommand = new DelegateCommand(() =>
            {
                CurNum = CenterNum;
                IsEnabledLeft = false;
                IsEnabledCenter = false;
                IsEnabledRight = true;
                IsEnabledStart = false;
                IsEnabledSwitch = true;
                IsEnabledQuit = true;
                Judge();
            });

            RightCommand = new DelegateCommand(() =>
            {
                CurNum = RightNum;
                IsEnabledLeft = false;
                IsEnabledCenter = false;
                IsEnabledRight = false;
                IsEnabledStart = false;
                IsEnabledSwitch = false;
                IsEnabledQuit = true;
                if (!Judge())
                {
                    Compute();
                }
            });

            StartCommand = new DelegateCommand(() =>
            {
                Player = E_Player.Man;
                Turn = GetTurn();
                CurNum = 0;
                LeftNum = 1;
                CenterNum = 2;
                RightNum = 3;
                IsEnabledLeft = true;
                IsEnabledCenter = false;
                IsEnabledRight = false;
                IsEnabledStart = false;
                IsEnabledSwitch = false;
                IsEnabledQuit = true;
            });

            SwitchCommand = new DelegateCommand(() =>
            {
                IsEnabledLeft = false;
                IsEnabledCenter = false;
                IsEnabledRight = false;
                IsEnabledStart = false;
                IsEnabledSwitch = false;
                IsEnabledQuit = true;
                Compute();
            });

            QuitCommand = new DelegateCommand(() =>
            {
                Quit();
            });
        }

        private void Quit()
        {
            Player = E_Player.Man;
            Turn = GetTurn();
            CurNum = 0;
            LeftNum = 1;
            CenterNum = 2;
            RightNum = 3;
            IsEnabledLeft = false;
            IsEnabledCenter = false;
            IsEnabledRight = false;
            IsEnabledStart = true;
            IsEnabledSwitch = false;
            IsEnabledQuit = false;
        }

        private void Compute()
        {
            int count = 0;

            Player = E_Player.Computer;
            Turn = GetTurn();

            Random r = new Random();
            int times = r.Next(3) + 1;

            Device.StartTimer(TimeSpan.FromSeconds(SEC), () =>
            {
                bool ret = false;

                if (++count <= times)
                {
                    if (++CurNum < MAX) ret = true;
                }

                if (Judge())
                {
                    Quit();
                }

                if(!ret) 
                {
                    ret =  false;
                    Player = E_Player.Man;
                    Turn = GetTurn();
                    LeftNum = CurNum + 1;
                    CenterNum = CurNum + 2;
                    RightNum = CurNum + 3;
                    IsEnabledLeft = true;
                    IsEnabledCenter = false;
                    IsEnabledRight = false;
                    IsEnabledStart = false;
                    IsEnabledSwitch = false;
                    IsEnabledQuit = true;
                }
                return ret;
            });
        }

        private string GetTurn() => $"{(Player == E_Player.Man ? "あなた" : "コンピュータ")}の番";

        private bool Judge (bool display = true)
        {
            bool ret = false;
            if (CurNum == MAX)
            {
                ret = true;
                if (display)
                {
                    DialogService.DisplayAlertAsync(
                        "Count30",
                        $"あなたの{(Player == E_Player.Man ? "負け" : "勝ち")}",
                        "閉じる");
                }
            }
            return ret;
        }
    }
}
