using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MathApp
{

    public partial class MainPage : ContentPage
    {
        Memory game;
        bool flag; // когда true sequence_entry_TextChanged не будет выполняться; для того, чтобы логика в этой функции работала только когда нужно вводить число
        bool flagReadOnly; // чтобы нельзя было вводить когда это нам не нужно

        public MainPage(string action) // конструктор
        {
            InitializeComponent();
            Device.StartTimer(new TimeSpan(0, 0, 1), Callback);
            flagReadOnly = true;
            flag = true;

            switch (action) // в зависимости от выбранного уровня сложности создаём объект класса Memory и передаем скольки значное число будем генерировать
            {
                case "Легкий":
                    game = new Memory(1, 3);
                    break;
                case "Средний":
                    game = new Memory(1, 5);
                    break;
                case "Cложный":
                    game = new Memory(1, 7);
                    break;
            }

            InitializeRandomNumber();
        }

        private bool Callback() // запускается Device.StartTimer в конструкторе, вызывается раз в секунду до конца отведенного времени
        {
            game.TimeTick();
            time_label.Text = game.Time.Minutes + ":" + string.Format("{0:d2}", game.Time.Seconds);
            if (game.Time.TotalSeconds == 0) // если время закончилось
            {
                ExitGame();
                return false;
            }
                
            return true;
        }

        private async void ExitGame() // вызывем уведомление, а потом закрываем окно MainPage
        {
            await DisplayAlert("Время закончилось", "Отлично сыграно!", "Спасибо!");
            await Navigation.PopModalAsync();
        }

        private bool InitializeRandomNumber() // рандомим число, записываем его в строку, через 3 секунды вызываем On3SecTick
        {
            sequence_entry.TextColor = Color.White;
            game.SetRandomNumber();
            sequence_entry.Text = game.CurrentNumber;
            Device.StartTimer(TimeSpan.FromMilliseconds(3000), On3SecTick);
            return false;
        }

        private bool On3SecTick() // стираем число со строки, даем возможность записывать что-то в строку и включаем логику sequence_entry_TextChanged
        {
            sequence_entry.Text = "";
            flag = false;
            flagReadOnly = false;
            return false;
        }

        private void sequence_entry_TextChanged(object sender, TextChangedEventArgs e) // будет срабатывать каждый раз, когда в строке для ввода будет меняться что-то
        {
            if (flag)
                return;

            if (sequence_entry.Text.Length == game.Level) // если количество знаков введеднных в строке = количеству знаков в загаданном числе
            {
                flagReadOnly = true;
                if (sequence_entry.Text == game.CurrentNumber) // если равны загаданное и написанное
                {
                    flag = true;
                    game.AddPoints();
                    points_label.Text = string.Format("{0,-4} {1,5}", "Очки:", game.Points);
                    sequence_entry.TextColor = Color.Green;
                    Device.StartTimer(TimeSpan.FromMilliseconds(2000), InitializeRandomNumber);
                }
                else
                {
                    flag = true;
                    game.SubtractPoints();
                    points_label.Text = string.Format("{0,-4} {1,5}", "Очки:", game.Points);
                    sequence_entry.TextColor = Color.Red;
                    Device.StartTimer(TimeSpan.FromMilliseconds(2000), InitializeRandomNumber);
                }
                
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e) // обработчик кнопок
        {
            Button button = sender as Button;

            if (!flagReadOnly)
                sequence_entry.Text += button.Text;
        }

        private void Button_Clicked_1(object sender, EventArgs e) // обработчик кнопки "С"
        {
            Button button = sender as Button;
            if (!flagReadOnly)
                sequence_entry.Text = "";
        }

        private async void Button_Clicked_2(object sender, EventArgs e) // обработчик кнопки "К меню"
        {
            await Navigation.PopModalAsync();
        }
    }
}
