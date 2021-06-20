using System;
using System.Collections.Generic;
using System.Text;

namespace MathApp
{
    public class Memory // класс игры
    {
        public TimeSpan Time { get; private set; }
        public int Points { get; private set; } = 0; // очки
        public string CurrentNumber { get; private set; } // текущее загаданное число
        public int Level { get; private set; } // уровень сложности
        //private int x = 1; // множитель очков
        private int minNumber;
        private int maxNumber;
        Random rand = new Random();

        public Memory(int minutes, int lev)
        {
            Time = new TimeSpan(0, minutes, 0);
            Level = lev;
            minNumber = Convert.ToInt32(Math.Pow(10, Level - 1));
            maxNumber = Convert.ToInt32(Math.Pow(10, Level));
        }

        public void TimeTick() // при вызове уменишает таймер на 1 секунду
        {
            Time = Time.Subtract(new TimeSpan(0, 0, 1));
        }

        public void AddPoints() // прибавляем очки, вызывается в случае правильного ответа
        {
            //Points += 100 * x;
            Points += 100;
            //x += 1;
        }

        public void SubtractPoints()
        {
            Points -= 100;
            //x = 1;
        }

        public void SetRandomNumber() // устанавливаем рандомное число 
        {
            CurrentNumber = rand.Next(minNumber, maxNumber).ToString();
        }
    }
}
