using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace snakeGame
{
    public class gameStatus
    {
        public int Rows { get; }
        public int Cols { get; }
        public gridValues[,] Grid { get; }
        public direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool gameOver { get; private set; }

        private readonly LinkedList<position> snakePositions = new LinkedList<position>();
        private readonly Random random = new Random();  

        public gameStatus(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new gridValues[rows, cols];
            Dir = direction.Right;

            addSnake();
            addFood();
        }

        private void addSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = gridValues.Snake;
                snakePositions.AddFirst(new position(r, c));
            }
        }

        private IEnumerable<position> emptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == gridValues.Empty)
                    {
                        yield return new position(r, c);
                    }
                }
            }
        }

        private void addFood()
        {
            List<position> empty = new List<position>(emptyPositions());

            if (empty.Count == 0)
            {
                return;
            }

            position pos = empty[random.Next(empty.Count)];
            Grid[pos.row, pos.col] = gridValues.Food;
        }

        public position headPosition()
        {
            return snakePositions.First.Value;
        }

        public position tailPosition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<position> SnakePositions()
        {
            return snakePositions;
        }

        private void addHead(position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.row, pos.col] = gridValues.Snake;
        }

        private void removeTail()
        {
            position tail = snakePositions.Last.Value;
            Grid[tail.row, tail.col] = gridValues.Empty;
            snakePositions.RemoveLast();
        }

        public void ChangeDirection(direction dir)
        {
            Dir = dir;
        }

        private bool OutsideGrid(position pos)
        {
            return pos.row < 0 || pos.row >= Rows || pos.col < 0 || pos.col >= Cols;
        }

        private gridValues WillHit(position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return gridValues.Outside;
            }

            if (newHeadPos == tailPosition())
            {
                return gridValues.Empty;
            }

            return Grid[newHeadPos.row, newHeadPos.col];
        }

        public void Move()
        {
            position newHeadPos = headPosition().Translate(Dir);
            gridValues hit = WillHit(newHeadPos);

            if (hit == gridValues.Outside || hit == gridValues.Snake)
            {
                gameOver = true;
            }
            else if (hit == gridValues.Empty)
            {
                removeTail();
                addHead(newHeadPos);
            }
            else if (hit == gridValues.Food)
            {
                addHead(newHeadPos);
                Score++;
                addFood();
            }
        }
    }
}
