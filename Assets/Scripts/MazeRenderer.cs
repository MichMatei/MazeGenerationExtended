using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    //[SerializeField]
    //[Range(1, 100)]
    private int height;

    [SerializeField]
    [Range(1, 100)]
    private int width = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab;

    [SerializeField]
    private Transform trailWallPrefab;

    [SerializeField]
    private Transform theCube;

    [SerializeField]
    private GameObject finishLine;
    int randomSide;
    int randomLine;
    int randomColumn;
    int iFinish;
    int jFinish;

    Vector3 position;
    Vector3 positionForNewRow;

    Vector3 topPositionSaved;
    Vector3 downPositionSaved;
    Vector3 leftPositionSaved;
    Vector3 rightPositionSaved;
    Quaternion upDownRotationSaved;
    Quaternion leftRightRotationSaved;

    public static int widthCopy;

    public static Cell[,] playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        var startMenuGameObject = GameObject.Find("StartMenuCanvas").gameObject;
        
        width = startMenuGameObject.GetComponent<StartMenu>().width;
        height = width * 6;
        positionForNewRow = position;
        theCube.localScale = Vector3.one * width;
        theCube.position = Vector3.right * ((float)width / 2 - 0.5f) + Vector3.up * (((float)width / 2 + 0.5f) * -1)
                         + Vector3.forward * ((float)width / 2 - 0.5f);

        widthCopy = width;
        randomSide = Random.Range(0, 6);
        randomLine = Random.Range(0, width);
        randomColumn = Random.Range(0, width);
        iFinish = randomSide * width + randomLine;
        jFinish = randomColumn;

        var maze = EllersGenerator.Generate(height, width);
        Draw(maze);

        playerPosition = maze;

        Destroy(startMenuGameObject);
    }



    private void Draw(Cell[,] maze)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var cell = maze[i, j];

                //The following if statements ammend variables that handle how to spawn the walls, accordingly to what side we are curently rendering.
                if (i < width)
                {
                    topPositionSaved = position + (Vector3.forward * size / 2);
                    downPositionSaved = position + (Vector3.back * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(0, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 2)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 3)
                {
                    topPositionSaved = position + (Vector3.back * size / 2);
                    downPositionSaved = position + (Vector3.forward * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(0, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 4)
                {
                    topPositionSaved = position + (Vector3.up * size / 2);
                    downPositionSaved = position + (Vector3.down * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 5)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.back * size / 2);
                    rightPositionSaved = position + (Vector3.forward * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 0, 0);
                }
                else if (i < width * 6)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.forward * size / 2);
                    rightPositionSaved = position + (Vector3.back * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 0, 0);
                }

                //The next if statements handle the instantiating of walls.
                //The next else statmens in the regions were used for debugging purposes.
                if (cell.TopWall == true)
                {
                    var topWall = Instantiate(wallPrefab, transform);
                    topWall.name = "TopWall of Cell[" + i + "][" + j + "] SV:"+cell.setValue;
                    topWall.position = topPositionSaved;
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                    topWall.rotation = upDownRotationSaved;
                }
                #region
                //else if (cell.TopWall == false)
                //{
                //    var topWall = Instantiate(wallPrefab, transform);
                //    topWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    topWall.name = "TopWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                //    topWall.position = topPositionSaved;
                //    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                //    topWall.rotation = upDownRotationSaved;
                //}
                #endregion

                if (cell.BottomWall == true)
                {
                    var downWall = Instantiate(wallPrefab, transform);
                    downWall.name = "DownWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                    downWall.position = downPositionSaved;
                    downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    downWall.rotation = upDownRotationSaved;
                }
                #region
                //else if (cell.BottomWall == false)
                //{
                //    var downWall = Instantiate(wallPrefab, transform);

                //    downWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    downWall.name = "DownWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                //    downWall.position = downPositionSaved;
                //    downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                //    downWall.rotation = upDownRotationSaved;
                //}
                #endregion

                if (cell.LeftWall == true)
                {
                    var leftWall = Instantiate(wallPrefab, transform);
                    leftWall.name = "LeftWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                    leftWall.position = leftPositionSaved;
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.rotation = leftRightRotationSaved;
                }
                #region
                //else if (cell.LeftWall == false)
                //{
                //    var leftWall = Instantiate(wallPrefab, transform);

                //    leftWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    leftWall.name = "LeftWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                //    leftWall.position = leftPositionSaved;
                //    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                //    leftWall.rotation = leftRightRotationSaved;
                //}
                #endregion

                if (cell.RightWall == true)
                {
                    var rightWall = Instantiate(wallPrefab, transform);
                    rightWall.name = "RightWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                    rightWall.position = rightPositionSaved;
                    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    rightWall.rotation = leftRightRotationSaved;
                }
                #region
                //else if (cell.RightWall == false)
                //{
                //    var rightWall = Instantiate(wallPrefab, transform);

                //    rightWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    rightWall.name = "RightWall of Cell[" + i + "][" + j + "] SV:" + cell.setValue;
                //    rightWall.position = rightPositionSaved;
                //    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                //    rightWall.rotation = leftRightRotationSaved;
                //}
                #endregion

                if (i == iFinish && j == jFinish)
                {
                    var finish = Instantiate(finishLine, transform);
                    finish.transform.position = position;
                }

                //The following statements change the position of the renderer for the next cell in the line acordigly to the side we are rendering.
                if (i < width * 4)
                {
                    position = position + Vector3.right;
                }
                else if (i < width * 5)
                {
                    position = position + Vector3.forward;
                }
                else
                {
                    position = position + Vector3.back;
                }
            }


            //The following if statements handle the position for the new next row.
            if (i < width - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.forward;
            }
            else if (i == width - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.forward;
                position = position + Vector3.down;
            }
            else if (i < width * 2 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            else if (i == width * 2 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
                position = position + Vector3.down;
            }
            else if (i < width * 3 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
            }
            else if (i == width * 3 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
                position = position + Vector3.up;
            }
            else if (i < width * 4 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.up;
            }
            else if (i == width * 4 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.left;
                position = position + Vector3.forward;
            }
            else if (i < width * 5 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            else if (i == width * 5 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.up * (width - 1);
                position = position + Vector3.right * (width + 1);
                position = position + Vector3.forward * (width - 1);
            }
            else if (i < width * 6 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            positionForNewRow = position;
        }
    }

    public static int CheckAvailableMoves(int i, int j)
    {
        int result = 10000;

        //The next if statements ammend the result variable in a way that if there is a top Wall, the first 0 in result, from left to right, will be changed to a 1, by adding
        //1000 to result. Second 0 is for down wall and so on...
        if (playerPosition[i, j].TopWall == false)
        {
            result += 1000;
        }

        if (playerPosition[i, j].BottomWall == false)
        {
            result += 100;
        }

        if (playerPosition[i, j].LeftWall == false)
        {
            result += 10;
        }

        if (playerPosition[i, j].RightWall == false)
        {
            result += 1;
        }

        return result;
    }

    public static int GetWidth(int smallWidth)
    {
        smallWidth = widthCopy;

        return smallWidth;
    }
}
