using System.Collections.Generic;
using UnityEngine;

public struct Cell
{
    public bool TopWall;
    public bool BottomWall;
    public bool LeftWall;
    public bool RightWall;

    public int setValue;
}
public static class EllersGenerator
{
    private static Cell[,] ApplyEllers(Cell[,] maze, int height, int width)
    {
        List<int> myCellList = new List<int>();

        int i, j;

        for (j = 0; j < width; j++)
        {
            maze[0, j].setValue = j;
        }

        for (i = 0; i < height - 1; i++)
        {
            for (j = 0; j < width - 1; j++)
            {
                int chanceForWall = Random.Range(0, 101);

                if (chanceForWall < 50 || maze[i, j].setValue == maze[i, j + 1].setValue) // add a wall
                {
                    maze[i, j].RightWall = true;
                    maze[i, j + 1].LeftWall = true;
                }
                else //make conection
                {
                    maze[i, j + 1].setValue = maze[i, j].setValue;
                }
            }

            for (j = 0; j < width - 1; j++)
            {
                //if current and next cell are of the same set, we add them both to the list
                if (maze[i, j].setValue == maze[i, j + 1].setValue)
                {
                    //both the current and next cell's top walls and the northern neighbourn bottom walls are turned on, then later we will remove them as we
                    //make the connections
                    maze[i, j].TopWall = true;
                    maze[i + 1, j].BottomWall = true;
                    maze[i, j + 1].TopWall = true;
                    maze[i + 1, j + 1].BottomWall = true;

                    if (myCellList.Count == 0)
                    {
                        myCellList.Add(j);
                        myCellList.Add(j + 1);
                    }
                    else
                    {
                        myCellList.Add(j + 1);
                    }
                }
                else //if they are not, we make random conections upwards
                {
                    if (myCellList.Count > 1)
                    {
                        for (int c = 0; c < myCellList.Count; c++)
                        {
                            int a = Random.Range(0, myCellList.Count);
                            int value = myCellList[a];

                            maze[i, value].TopWall = false;
                            maze[i + 1, value].BottomWall = false;
                        }
                    }

                    myCellList.Clear();
                }
            }
            
            //This prevents the algorithm moving to the next line without running the conections code in case we are on the last two cells
            //and they are both of the same set
            if (myCellList.Count > 1)
            {
                for (int c = 0; c < myCellList.Count; c++)
                {
                    int a = Random.Range(0, myCellList.Count);
                    int value = myCellList[a];

                    maze[i, value].TopWall = false;
                    maze[i + 1, value].BottomWall = false;
                }

                myCellList.Clear();
            }

            //iterate trough all cells in the line, if they don't have a top wall, the cell above gets the current cell's set value, else, it gets a new set value
            for (j = 0; j < width; j++)
            {
                if (maze[i, j].TopWall == false)
                {
                    maze[i + 1, j].setValue = maze[i, j].setValue;
                }
                else if (maze[i, j].TopWall == true)
                {
                    maze[i + 1, j].setValue = (i + 1) * 10 + j;
                }
            }
        }
       
        //most likely obsolete as we delete all of last line's right walls.
        for (j = 0; j < width; j++)
        {
            maze[height - 1, j].RightWall = maze[height - 2, j].RightWall;
        }

        //same as the if statement above
        for (j = 0; j < width - 1; j++)
        {
            if (maze[height - 1, j].setValue != maze[height - 1, j + 1].setValue)
            {
                maze[height - 1, j].RightWall = false;
                maze[height - 1, j + 1].LeftWall = false;
            }
        }

        //if uncommented, only the corners will be true
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                if(i == width * 5)
                {
                    maze[i, j].BottomWall = true;
                    maze[i, j].RightWall = false;
                    maze[i, j].LeftWall = false;
                }

                if (i == width * 4)
                {
                    maze[i, j].BottomWall = true;
                    maze[i, j].RightWall = false;
                    maze[i, j].LeftWall = false;
                }

                if (i == width * 5 - 1)
                {
                    maze[i, j].TopWall = true;
                    maze[i, j].RightWall = false;
                    maze[i, j].LeftWall = false;
                }

                if (i == width * 4 - 1)
                {
                    maze[i, j].TopWall = true;
                    maze[i, j].RightWall = false;
                    maze[i, j].LeftWall = false;
                }
            }
        }

        //working staticly for a 10 width maze, will modify later  /// still need to conect both ways 
        maze[0, width / 2].BottomWall = false;
        maze[width * 4 - 1, width / 2].TopWall = false;

        maze[width / 2, 0].LeftWall = false;
        maze[width * 4, width / 2].BottomWall = false;


        maze[width / 2, width - 1].RightWall = false;               // [5,9]
        maze[width * 5, width - 1 - (width / 2)].BottomWall = false;  // [25,5]

        //Blocking every sides's corners off
        //First Side
        maze[0, 0].BottomWall = true; maze[0, 0].LeftWall = true;
        maze[0, width - 1].BottomWall = true; maze[0, width - 1].RightWall = true;

        maze[width - 1, 0].TopWall = false; maze[width - 1, 0].LeftWall = true;
        maze[width - 1, width - 1].TopWall = false; maze[width - 1, width - 1].RightWall = true;

        //Second Side
        maze[width, 0].BottomWall = false; maze[width, 0].LeftWall = true;
        maze[width, width - 1].BottomWall = false; maze[width, width - 1].RightWall = true;

        maze[width * 2 - 1, 0].TopWall = false; maze[width * 2 - 1, 0].LeftWall = true;
        maze[width * 2 - 1, width - 1].TopWall = false; maze[width * 2 - 1, width - 1].RightWall = true;

        //Third Side
        maze[width * 2, 0].BottomWall = false; maze[width * 2, 0].LeftWall = true;
        maze[width * 2, width - 1].BottomWall = false; maze[width * 2, width - 1].RightWall = true;

        maze[width * 3 - 1, 0].TopWall = false; maze[width * 3 - 1, 0].LeftWall = true;
        maze[width * 3 - 1, width - 1].TopWall = false; maze[width * 3 - 1, width - 1].RightWall = true;

        //Fourth Side
        maze[width * 3, 0].BottomWall = false; maze[width * 3, 0].LeftWall = true;
        maze[width * 3, width - 1].BottomWall = false; maze[width * 3, width - 1].RightWall = true;

        maze[width * 4 - 1, 0].TopWall = true; maze[width * 4 - 1, 0].LeftWall = true;
        maze[width * 4 - 1, width - 1].TopWall = true; maze[width * 4 - 1, width - 1].RightWall = true;

        //Fifth Side
        maze[width * 4, 0].BottomWall = true; maze[width * 4, 0].LeftWall = true;
        maze[width * 4, width - 1].BottomWall = true; maze[width * 4, width - 1].RightWall = true;

        maze[width * 5 - 1, 0].TopWall = true; maze[width * 5 - 1, 0].LeftWall = true;
        maze[width * 5 - 1, width - 1].TopWall = true; maze[width * 5 - 1, width - 1].RightWall = true;

        //Sixth Side
        maze[width * 5, 0].BottomWall = true; maze[width * 5, 0].LeftWall = true;
        maze[width * 5, width - 1].BottomWall = true; maze[width * 5, width - 1].RightWall = true;

        maze[width * 6 - 1, 0].TopWall = true; maze[width * 6 - 1, 0].LeftWall = true;
        maze[width * 6 - 1, width - 1].TopWall = true; maze[width * 6 - 1, width - 1].RightWall = true;

        return maze;
    }

    public static Cell[,] Generate(int height, int width)
    {
        Cell[,] maze = new Cell[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j].TopWall = false;
                maze[i, j].BottomWall = false;
                maze[i, j].LeftWall = false;
                maze[i, j].RightWall = false;

                if (i == 0)
                {
                    maze[i, j].BottomWall = true;
                }
                if (i == height - 1)
                {
                    maze[i, j].TopWall = true;
                }
                if (j == 0)
                {
                    maze[i, j].LeftWall = true;
                }
                if (j == width - 1)
                {
                    maze[i, j].RightWall = true;
                }
            }
        }

        return ApplyEllers(maze, height, width);
    }
}