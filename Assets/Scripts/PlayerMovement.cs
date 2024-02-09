using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerMovementInstance;
    public GameObject startMenuCanvas;
    [SerializeField] int i = 0;
    [SerializeField] int j = 0;
    public int b;
    public int sideNumber;

    [SerializeField] GameObject trailPrefab;
    [SerializeField] GameObject trailPosition;
    [SerializeField] Transform cameraPosition;

    Vector3 forwardMovement;
    Vector3 backwardMovement;
    Vector3 leftMovement;
    Vector3 rightMovement;

    int width;
    float maximumTrailItems;
    bool correctMovement = false;

    [SerializeField] Image northDir;
    [SerializeField] Image southDir;
    [SerializeField] Image westDir;
    [SerializeField] Image eastDir;

    Color greenColor;
    Color redColor;

    public bool northAvailable;
    bool southAvailable;
    bool westAvailable;
    bool eastAvailable;

    public bool reachedEnd = false;
    public bool gamePaused = false;

    [SerializeField] List<GameObject> trailItems;

    enum CurrentSide
    {
        firstSide,
        secondSide,
        thirdSide,
        fourthSide,
        fifthSide,
        sixthSide,
        firstSecond,
        secondFirst,
        secondThird,
        thirdSecond,
        thirdFourth,
        fourthThird,
        fourthFirst,
        firstFourth,
        firstFifth,
        fifthFirst,
        firstSixth,
        sixthFirst,
    };

    CurrentSide currentSide = new CurrentSide();

    private void Awake()
    {
        if (playerMovementInstance == null)
        {
            playerMovementInstance = this;
            DontDestroyOnLoad(playerMovementInstance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        width = MazeRenderer.widthCopy;

        currentSide = CurrentSide.firstSide;
        trailItems = new();
        maximumTrailItems = width * 2 + width / 2;

        cameraPosition.position = new Vector3(0, width, 0);

        greenColor = northDir.color;
        redColor = southDir.color;


        correctMovement = true;
        PositionIncrease(currentSide);
        InstantiateTrailPrefab();

        reachedEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        //The next if statements handle player movement
        if (Input.GetKeyDown(KeyCode.W) && !reachedEnd && !gamePaused)
        {
            PositionIncrease(currentSide);

            if (currentSide == CurrentSide.fourthFirst && b / 1000 % 10 == 1)
            {
                i = 0;
                transform.position += forwardMovement;
                correctMovement = true;
            }
            else if (b / 1000 % 10 == 1)
            {
                i++;
                transform.position += forwardMovement;
                correctMovement = true;
            }
            else
            {
                //Error: An available path doesn't exist
                correctMovement = false;
            }
            InstantiateTrailPrefab();
        }

        if (Input.GetKeyDown(KeyCode.S) && !reachedEnd && !gamePaused)
        {
            PositionIncrease(currentSide);

            if(currentSide == CurrentSide.firstFourth && b / 100 % 10 == 1)
            {
                i = width * 4 - 1;
                transform.position += backwardMovement;
                correctMovement = true;
            }
            else if (currentSide == CurrentSide.fifthFirst && b / 100 % 10 == 1)
            {
                i = j;
                j = 0;
                transform.position += backwardMovement;
                correctMovement = true;
            }
            else if (currentSide == CurrentSide.sixthFirst && b / 100 % 10 == 1)
            {
                i = (width - 1) - j;
                j = width - 1;
                transform.position += backwardMovement;
                correctMovement = true;
            }
            else if (b / 100 % 10 == 1)
            {
                i--;
                transform.position += backwardMovement;
                correctMovement = true;
            }
            else
            {
                //Error: An available path doesn't exist
                correctMovement = false;
            }
            InstantiateTrailPrefab();
        }

        if (Input.GetKeyDown(KeyCode.A) && !reachedEnd && !gamePaused)
        {
            PositionIncrease(currentSide);

            if(currentSide == CurrentSide.firstFifth && b / 10 % 10 == 1)
            {
                j = i;
                i = width * 4;
                transform.position += leftMovement;
                correctMovement = true;
            }
            else if (b / 10 % 10 == 1)
            {
                j--;
                transform.position += leftMovement;
                correctMovement = true;
            }
            else
            {
                //Error: An available path doesn't exist
                correctMovement = false;
            }
            InstantiateTrailPrefab();
        }

        if (Input.GetKeyDown(KeyCode.D) && !reachedEnd && !gamePaused)
        {
            PositionIncrease(currentSide);

            if (currentSide == CurrentSide.firstSixth && b % 10 == 1)
            {
                j = (width - 1) - i;
                i = width * 5;
                transform.position += rightMovement;
                correctMovement = true;
            }
            else if (b % 10 == 1)
            {
                j++;
                transform.position += rightMovement;
                correctMovement = true;
            }
            else
            {
                //Error: An available path doesn't exist
                correctMovement = false;
            }
            InstantiateTrailPrefab();
        }
    }

    private void FixedUpdate()
    {
        b = MazeRenderer.CheckAvailableMoves(i, j);
        UpdateDirections();
        PositionIncrease(currentSide);
    }

    //Depending on what side the player is on, or going to transition to, we change the variables to match the needed values.
    void PositionIncrease (CurrentSide currentSide)
    {
        AmmendCurrentSide();
        switch (currentSide)
        {
            case CurrentSide.firstSide:
                forwardMovement = Vector3.forward;
                backwardMovement = Vector3.back;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 1;
                break;
            case CurrentSide.secondSide:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 2;
                break;
            case CurrentSide.thirdSide:
                forwardMovement = Vector3.back;
                backwardMovement = Vector3.forward;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 3;
                break;
            case CurrentSide.fourthSide:
                forwardMovement = Vector3.up;
                backwardMovement = Vector3.down;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 4;
                break;
            case CurrentSide.fifthSide:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up;
                leftMovement = Vector3.back;
                rightMovement = Vector3.forward;
                sideNumber = 5;
                break;
            case CurrentSide.sixthSide:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up;
                leftMovement = Vector3.forward;
                rightMovement = Vector3.back;
                sideNumber = 6;
                break;
            case CurrentSide.firstSecond:
                forwardMovement = Vector3.forward + Vector3.down;
                backwardMovement = Vector3.back;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 1;
                break;
            case CurrentSide.secondFirst:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up + Vector3.back;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 2;
                break;
            case CurrentSide.secondThird:
                forwardMovement = Vector3.down + Vector3.back;
                backwardMovement = Vector3.up;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 2;
                break;
            case CurrentSide.thirdSecond:
                forwardMovement = Vector3.back;
                backwardMovement = Vector3.forward + Vector3.up;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 3;
                break;
            case CurrentSide.thirdFourth:
                forwardMovement = Vector3.back + Vector3.up;
                backwardMovement = Vector3.forward;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 3;
                break;
            case CurrentSide.fourthThird:
                forwardMovement = Vector3.up;
                backwardMovement = Vector3.down + Vector3.forward;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 4;
                break;
            case CurrentSide.fourthFirst:
                forwardMovement = Vector3.up + Vector3.forward;
                backwardMovement = Vector3.down;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 4;
                break;
            case CurrentSide.firstFourth:
                forwardMovement = Vector3.forward;
                backwardMovement = Vector3.back + Vector3.down;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right;
                sideNumber = 1;
                break;
            case CurrentSide.firstFifth:
                forwardMovement = Vector3.forward;
                backwardMovement = Vector3.back;
                leftMovement = Vector3.left + Vector3.down;
                rightMovement = Vector3.right;
                sideNumber = 1;
                break;
            case CurrentSide.fifthFirst:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up + Vector3.right;
                leftMovement = Vector3.back;
                rightMovement = Vector3.forward;
                sideNumber = 5;
                break;
            case CurrentSide.firstSixth:
                forwardMovement = Vector3.forward;
                backwardMovement = Vector3.back;
                leftMovement = Vector3.left;
                rightMovement = Vector3.right + Vector3.down;
                sideNumber = 1;
                break;
            case CurrentSide.sixthFirst:
                forwardMovement = Vector3.down;
                backwardMovement = Vector3.up + Vector3.left;
                leftMovement = Vector3.forward;
                rightMovement = Vector3.back;
                sideNumber = 6;
                break;
            default:
                break;
        }
    }

    //This function updates the rotation of the player in relation to what side he is on
    void AmmendCurrentSide()
    {
        if (i < width - 1 && j == 0)
        {
            currentSide = CurrentSide.firstFifth;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i < width - 1 && j == width - 1)
        {
            currentSide = CurrentSide.firstSixth;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == 0)
        {
            currentSide = CurrentSide.firstFourth;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i < width - 1)
        {
            currentSide = CurrentSide.firstSide;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == width - 1)
        {
            currentSide = CurrentSide.firstSecond;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (i == width)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            currentSide = CurrentSide.secondFirst;
        }
        else if (i < width * 2 - 1)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            currentSide = CurrentSide.secondSide;
        }
        else if (i == width * 2 - 1)
        {
            currentSide = CurrentSide.secondThird;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else if (i == width * 2)
        {
            currentSide = CurrentSide.thirdSecond;
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else if (i < width * 3 - 1)
        {
            currentSide = CurrentSide.thirdSide;
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else if (i == width * 3 - 1)
        {
            currentSide = CurrentSide.thirdFourth;
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else if (i == width * 3)
        {
            currentSide = CurrentSide.fourthThird;
            transform.rotation = Quaternion.Euler(270, 0, 0);
        }
        else if (i < width * 4 - 1)
        {
            currentSide = CurrentSide.fourthSide;
            transform.rotation = Quaternion.Euler(270, 0, 0);
        }
        else if (i == width * 4 - 1)
        {
            currentSide = CurrentSide.fourthFirst;
            transform.rotation = Quaternion.Euler(270, 0, 0);
        }
        else if (i == width * 4)
        {
            currentSide = CurrentSide.fifthFirst;
            transform.rotation = Quaternion.Euler(90, -90, 0);
        }
        else if (i < width * 5)
        {
            currentSide = CurrentSide.fifthSide;
            transform.rotation = Quaternion.Euler(90, -90, 0);
        }
        else if (i == width * 5)
        {
            currentSide = CurrentSide.sixthFirst;
            transform.rotation = Quaternion.Euler(90, 90, 0);
        }
        else if (i < width * 6)
        {
            currentSide = CurrentSide.sixthSide;
            transform.rotation = Quaternion.Euler(90, 90, 0);
        }
    }

    //Handles the instatiation of the trail prefabs and handles the list that hold all instantiated prefabs
    void InstantiateTrailPrefab()
    {
        if (!correctMovement)
        {
            return;
        }

        AmmendCurrentSide();

        GameObject trail = Instantiate(trailPrefab);
        trail.transform.localPosition = trailPosition.transform.position;
        trail.transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);
        trailItems.Add(trail);

        foreach (GameObject gameObject in trailItems)
        {
            Color myColor = gameObject.GetComponentInChildren<Image>().color;
            myColor.a -= 1.0f / maximumTrailItems;
            gameObject.GetComponentInChildren<Image>().color = new Color(myColor.r, myColor.g, myColor.b, myColor.a);
            gameObject.name = myColor.a.ToString();
        }

        if (trailItems.Count > maximumTrailItems)
        {
            Destroy(trailItems[0]);
            trailItems.RemoveAt(0);
        }

        correctMovement = false;
    }

    //Turn the UI arrows red or green if there is or not a wall in that direction
    public void UpdateDirections()
    {
        if (b / 1000 % 10 == 1)
        {
            northDir.color = greenColor;
            northAvailable = true;
        }
        else
        {
            northDir.color = redColor;
            northAvailable = false;
        }

        if (b / 100 % 10 == 1)
        {
            southDir.color = greenColor;
            southAvailable = true;
        }
        else
        {
            southDir.color = redColor;
            southAvailable = false;
        }

        if (b / 10 % 10 == 1)
        {
            westDir.color = greenColor;
            westAvailable = true;
        }
        else
        {
            westDir.color = redColor;
            westAvailable = false;
        }

        if (b % 10 == 1)
        {
            eastDir.color = greenColor;
            eastAvailable = true;
        }
        else
        {
            eastDir.color = redColor;
            eastAvailable = false;
        }
    }

    public bool ReturnNorthValue(bool available)
    {
        available = northAvailable;
        return available;
    }

    public bool ReturnSouthValue(bool available)
    {
        available = southAvailable;
        return available;
    }
    public bool ReturnWestValue(bool available)
    {
        available = westAvailable;
        return available;
    }
    public bool ReturnEastValue(bool available)
    {
        available = eastAvailable;
        return available;
    }
}
