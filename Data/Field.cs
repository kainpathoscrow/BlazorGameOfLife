using System;

namespace BlazorGameOfLife.Data
{
    public enum CellStatus
    {
        Dead,
        Alive
    }

    public class Field
    {
        public int Width {get; }
        public int Height {get; }

        private CellStatus[,] field;

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.field = new CellStatus[height,width];
            this.SetClear();
        } 

        public CellStatus this[int x, int y]
        {
            get
            {
                return field[x, y];
            }
        }
        
        public void SetClear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    field[i, j] = CellStatus.Dead;
                }
            }
        }

        public void SwapCellStatus(int x, int y)
        {
            field[x, y] = field[x, y] == CellStatus.Alive ? CellStatus.Dead : CellStatus.Alive;
        }

        public void MakeTurn()
        {
            CellStatus[,] newField = new CellStatus[Height,Width];
            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    int numberOfAliveNeightbours = this.countAliveNeightbours(x, y);
                    if (field[x, y] == CellStatus.Dead && numberOfAliveNeightbours == 3)
                        newField[x, y] = CellStatus.Alive;
                    else if (field[x, y] == CellStatus.Alive && (numberOfAliveNeightbours < 2 || numberOfAliveNeightbours > 3))
                        newField[x, y] = CellStatus.Dead;
                    else 
                        newField[x, y] = field[x, y];
                }
            }
            this.field = newField;
        }
        
        private int countAliveNeightbours(int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x+1; i++)
            {
                int tempI = i;
                if (tempI == -1) tempI = Height - 1;
                if (tempI == Height) tempI = 0;
                for (int j = y - 1; j <= y+1; j++)
                {
                    int tempJ = j;
                    if (tempI == x && tempJ == y) continue;
                    if (tempJ == -1) tempJ = Width - 1;
                    if (tempJ == Width) tempJ = 0;

                    if (field[tempI, tempJ] == CellStatus.Alive)
                        count++;
                }
            }
            return count;
        }
    }
}
