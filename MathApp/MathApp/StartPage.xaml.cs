using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage // начальная страница, вызывается из App.xaml.cs
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void play_button_Clicked(object sender, EventArgs e) // открываем меню выбора уровня сложности и после этого запускаем окошко с игрой, передавая туда аргументом строку сложности
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            var action = await DisplayActionSheet("Выберите уровень", "Отменить", null, "Легкий", "Средний", "Cложный");
            if (action.Equals("Отменить"))
            {
                btn.IsEnabled = true;
            }
            else
            {
                await Navigation.PushModalAsync(new MainPage(action));
                btn.IsEnabled = true;
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e) // закрываем игру
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}