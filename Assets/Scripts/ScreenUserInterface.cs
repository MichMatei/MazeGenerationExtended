using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScreenUserInterface : MonoBehaviour
{
    PlayerMovement playerMovementInstance;

    [SerializeField] Image northArrow;
    [SerializeField] Image southArrow;
    [SerializeField] Image westArrow;
    [SerializeField] Image eastArrow;

    [SerializeField] Image northConnection;
    [SerializeField] Image southConnection;
    [SerializeField] Image westConnection;
    [SerializeField] Image eastConnection;
    [SerializeField] TextMeshProUGUI sideNumber;

    [SerializeField] Color redColor;
    [SerializeField] Color greenColor;

    bool northAvailable;
    bool southAvailable;
    bool westAvailable;
    bool eastAvailable;

    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementInstance = PlayerMovement.playerMovementInstance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseMenu();
        }
    }

    private void FixedUpdate()
    {
        ShowUIInfo();
    }

    //Handles the collors for UI elements
    void ShowUIInfo()
    {
        northAvailable = playerMovementInstance.ReturnNorthValue(northAvailable);
        southAvailable = playerMovementInstance.ReturnSouthValue(southAvailable);
        westAvailable = playerMovementInstance.ReturnWestValue(westAvailable);
        eastAvailable = playerMovementInstance.ReturnEastValue(eastAvailable);

        if(northAvailable)
        {
            northArrow.color = greenColor;
        }
        else
        {
            northArrow.color = redColor;
        }

        if (southAvailable)
        {
            southArrow.color = greenColor;
        }
        else
        {
            southArrow.color = redColor;
        }

        if (westAvailable)
        {
            westArrow.color = greenColor;
        }
        else
        {
            westArrow.color = redColor;
        }

        if (eastAvailable)
        {
            eastArrow.color = greenColor;
        }
        else
        {
            eastArrow.color = redColor;
        }

        sideNumber.text = playerMovementInstance.sideNumber.ToString();

        switch (playerMovementInstance.sideNumber)
        {
            case 1:
                northConnection.color = greenColor;
                southConnection.color = greenColor;
                westConnection.color = greenColor;
                eastConnection.color = greenColor;
                break;
            case 2:
                northConnection.color = greenColor;
                southConnection.color = greenColor;
                westConnection.color = redColor;
                eastConnection.color = redColor;
                break;
            case 3:
                northConnection.color = greenColor;
                southConnection.color = greenColor;
                westConnection.color = redColor;
                eastConnection.color = redColor;
                break;
            case 4:
                northConnection.color = greenColor;
                southConnection.color = greenColor;
                westConnection.color = redColor;
                eastConnection.color = redColor;
                break;
            case 5:
                northConnection.color = redColor;
                southConnection.color = greenColor;
                westConnection.color = redColor;
                eastConnection.color = redColor;
                break;
            case 6:
                northConnection.color = redColor;
                southConnection.color = greenColor;
                westConnection.color = redColor;
                eastConnection.color = redColor;
                break;

            default:
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Function for a button
    public void PlayAgain()
    {
        //We destroy the current instance of the player and load the StartMenu scene
        GameObject toBeDestroyed = GameObject.Find("PlayerCube");
        Destroy(toBeDestroyed);

        SceneManager.LoadScene("StartMenuScene");
    }

    public void PauseMenu()
    {
        if (pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            playerMovementInstance.gamePaused = true;
        }
        else if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
            playerMovementInstance.gamePaused = false;
        }
    }
}
