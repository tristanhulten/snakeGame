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
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyboardEventArgs e)
        {

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
        
        
      
