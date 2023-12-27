using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcnPractic
{
    public partial class Form2 : Form
    {
        private Button[,] buttons; // Двумерный массив кнопок для представления поля
        private int currentPlayer; // Текущий игрок: 0 для Х, 1 для О
        private bool gameEnded; // Флаг окончания игры
        private int[] playerWins = new int[2]; // Массив для хранения побед каждого игрока

        public Form2()
        {
            InitializeComponent();
            InitializeGame();
            // Создание и настройка кнопки "Новая игра"
            Button newGameButton = new Button();
            newGameButton.Text = "Новая игра";
            newGameButton.Location = new System.Drawing.Point(10, 250);
            newGameButton.Size = new System.Drawing.Size(120, 30);
            newGameButton.Click += NewGameButton_Click;
            Controls.Add(newGameButton);
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
        private void ResetGame()
        {
            // Очистка игрового поля (текст кнопок и состояние игры)
            foreach (Button button in buttons)
            {
                button.Text = "";
            }

            currentPlayer = 0;
            gameEnded = false;
        }
        private void InitializeGame()
        {
            buttons = new Button[3, 3]; // Инициализация массива 3x3 кнопок

            // Создание кнопок и их размещение на форме
            const int buttonSize = 80;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button button = new Button();
                    button.Width = button.Height = buttonSize;
                    button.Location = new System.Drawing.Point(col * buttonSize, row * buttonSize);
                    button.Font = new System.Drawing.Font("Arial", 36F);
                    button.Click += Button_Click;
                    buttons[row, col] = button;
                    Controls.Add(button);
                }
            }

            currentPlayer = 0; // Игрок 1 начинает
            gameEnded = false;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Получение индекса нажатой кнопки в массиве buttons
            int clickedRow = -1, clickedCol = -1;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (buttons[row, col] == clickedButton)
                    {
                        clickedRow = row;
                        clickedCol = col;
                        break;
                    }
                }
            }

            if (clickedRow != -1 && clickedCol != -1 && buttons[clickedRow, clickedCol].Text == "" && !gameEnded)
            {
                // Установка X или O в соответствующую клетку
                buttons[clickedRow, clickedCol].Text = currentPlayer == 0 ? "X" : "O";

                // Проверка на победу после хода
                if (CheckForWin())
                {
                    gameEnded = true;
                    playerWins[currentPlayer]++; // Увеличение количества побед текущего игрока
                }
                if (CheckForWin())
                {
                    gameEnded = true;
                    MessageBox.Show($"Игрок {(currentPlayer == 0 ? "1" : "2")} победил!\nПобед у Игрока 1: {playerWins[0]}, у Игрока 2: {playerWins[1]}");
                }
                else
                {
                    currentPlayer = (currentPlayer + 1) % 2; // Смена игрока после хода
                }
            }
        }

        private bool CheckForWin()
        {
            // Проверка всех возможных победных комбинаций
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 0].Text == buttons[i, 2].Text)
                {
                    return true; // Победа по горизонтали
                }
                if (buttons[0, i].Text != "" && buttons[0, i].Text == buttons[1, i].Text && buttons[0, i].Text == buttons[2, i].Text)
                {
                    return true; // Победа по вертикали
                }
            }

            if (buttons[0, 0].Text != "" && buttons[0, 0].Text == buttons[1, 1].Text && buttons[0, 0].Text == buttons[2, 2].Text)
            {
                return true; // Победа по диагонали слева направо
            }
            if (buttons[0, 2].Text != "" && buttons[0, 2].Text == buttons[1, 1].Text && buttons[0, 2].Text == buttons[2, 0].Text)
            {
                return true; // Победа по диагонали справа налево
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Поиск всех открытых форм и их закрытие
            foreach (Form openForm in Application.OpenForms)
            {
                openForm.Close();
            }
        }
    }
}
