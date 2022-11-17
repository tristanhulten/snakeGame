using System;
using System.Collections.Generic;
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

namespace snakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<gridValues, ImageSource> GridValToImage = new()
        {
            { gridValues.Empty, Images.Empty},
            { gridValues.Snake, Images.Body },
            { gridValues.Food, Images.Food },
        };

        private readonly int rows = 15, cols = 15;
        private readonly Image[,] gridImages;
        private gameStatus GameStatus;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            GameStatus = new gameStatus(rows, cols);
        }
        
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            await GameLoop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (GameStatus.gameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.A:
                    GameStatus.ChangeDirection(direction.Left);
                    break;
                case Key.D:
                    GameStatus.ChangeDirection(direction.Right);
                    break;
                case Key.W:
                    GameStatus.ChangeDirection(direction.Up);
                    break;
                case Key.S:
                    GameStatus.ChangeDirection(direction.Down);
                    break;
            }

        }
        
        private async Task GameLoop()
        {
            while (!GameStatus.gameOver)
            {
                await Task.Delay(100);
                GameStatus.Move();
                Draw();
            }
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }
        
        private void Draw()
        {
            DrawGrid();
            ScoreText.Text = $"SCORE {GameStatus.Score}";
        }
        
        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    gridValues gridVal = GameStatus.Grid[r, c];
                    gridImages[r, c].Source = GridValToImage[gridVal]; 
                }
            }
        }
    }
}
        
        
      
