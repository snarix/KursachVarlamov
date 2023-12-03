using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KursachVarlamov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> numbersToShow;
        private List<int> incorrectNumbers;
        private int currentIndex;
        private int correctCount;
        private int currentNumberIndex;


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Start(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            ResultsTextBlock.Text = string.Empty;

            numbersToShow = GenerateRandomNumbers(5);
            currentIndex = 0;
            correctCount = 0;
            incorrectNumbers = new List<int>();

            await ShowNumbersAsync();
            await Task.Delay(1000);

            InputTextBox.IsEnabled = true;
            InputTextBox.Focus();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();

            while (currentIndex < numbersToShow.Count)
            {
                await Task.Delay(100);
            }

            timer.Stop();
            InputTextBox.IsEnabled = false;
            StartButton.IsEnabled = true;

            DisplayResults();
            incorrectNumbers.Clear();
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обработчик события таймера
            // Если таймер истек, прерываем игру
            InputTextBox.IsEnabled = false;
            DisplayResults();
            (sender as DispatcherTimer).Stop(); // Останавливаем таймер
        }

        private void DisplayResults()
        {
            string resultMessage = $"Показано чисел: {numbersToShow.Count}\n" +
                                  $"Введено правильно: {correctCount}";

            if (incorrectNumbers.Any())
            {
                string incorrectNumbersString = string.Join(", ", incorrectNumbers);
                resultMessage += $"\nВведены неправильно: {incorrectNumbersString}";
            }

            ResultsTextBlock.Text = resultMessage;
        }

        private List<int> GenerateRandomNumbers(int count)
        {
            var random = new Random();
            return Enumerable.Range(1, count).Select(_ => random.Next(1, 10)).ToList();
        }

        private async Task ShowNumbersAsync()
        {
            currentNumberIndex = 1;

            foreach (var number in numbersToShow)
            {
                NumberTextBlock.Text = $"Число {currentNumberIndex}: {number}";
                await Task.Delay(1000);
                NumberTextBlock.Text = string.Empty;
                await Task.Delay(500);
                currentNumberIndex++;
            }
        }

        private void InputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Ограничение ввода только цифр
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                if (currentIndex < numbersToShow.Count)
                {
                    if (int.TryParse(InputTextBox.Text, out int userInput))
                    {
                        if (userInput == numbersToShow[currentIndex])
                        {
                            correctCount++;
                        }
                        else
                        {
                            incorrectNumbers.Add(userInput);
                        }

                        currentIndex++;
                        InputTextBox.Text = string.Empty;
                    }
                    else
                    {
                        // Обработка некорректного ввода, например, ничего не введено или введено не число
                    }
                }

                if (currentIndex == numbersToShow.Count)
                {
                    InputTextBox.IsEnabled = false;
                    StartButton.IsEnabled = true;
                    DisplayResults();
                    incorrectNumbers.Clear();
                }
            }
        }
        
    }
}
