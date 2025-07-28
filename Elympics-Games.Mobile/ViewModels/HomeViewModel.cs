using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<CarouselModel> Items { get; set; }

        public HomeViewModel()
        {
            Items = new ObservableCollection<CarouselModel>
            {
                new CarouselModel
                {
                    Image = "lol_carousel.jpg",
                    Title = "Competition in League o Legends"
                },
                new CarouselModel
                {
                    Image = "cs2_carousel.png",
                    Title = "Competition in CS2"
                },
                new CarouselModel
                {
                    Image = "rl_carousel.jpg",
                    Title = "Competition in Rocket League"
                }
            };
        }

        [RelayCommand]
        private async Task NavigateToShowTeamsAsync()
        {
            await Shell.Current.GoToAsync("//ShowTeamsView");
        }

        [RelayCommand]
        private async Task NavigateToManageTeamsAsync()
        {
            await Shell.Current.GoToAsync("//ManageTeamsView");
        }
    }
}
