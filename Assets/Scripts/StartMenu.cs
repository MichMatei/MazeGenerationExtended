using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public int width;
    public Slider sliderWidth;
    [SerializeField] TextMeshProUGUI widthNumberTxt;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject instructionsMenu;
    [SerializeField] GameObject objectiveText;
    [SerializeField] GameObject controlsText;
    [SerializeField] GameObject aboutTheMazeText;
    [SerializeField] GameObject chooseAnOptionText;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        objectiveText.SetActive(false);
        controlsText.SetActive(false);
        aboutTheMazeText.SetActive(false);
        chooseAnOptionText.SetActive(false);
    }

    private void FixedUpdate()
    {
        widthNumberTxt.text = sliderWidth.value.ToString();
    }

    public void LoadGameplayScene()
    {
        width = (int)sliderWidth.value;
        SceneManager.LoadScene("SampleScene");
    }

    //Function for a button
    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }

    //Function for a button
    public void ShowInstructionsMenu()
    {
        instructionsMenu.SetActive(true);
        startMenu.SetActive(false);
        chooseAnOptionText.SetActive(true);
        objectiveText.SetActive(false);
        controlsText.SetActive(false);
        aboutTheMazeText.SetActive(false);
    }

    //Function for a button
    public void ObjectiveText()
    {
        objectiveText.SetActive(true);
        controlsText.SetActive(false);
        aboutTheMazeText.SetActive(false);
        chooseAnOptionText.SetActive(false);
    }

    //Function for a button
    public void ControlsText()
    {
        controlsText.SetActive(true);
        objectiveText.SetActive(false);
        aboutTheMazeText.SetActive(false);
        chooseAnOptionText.SetActive(false);
    }

    //Function for a button
    public void AboutTheMazeText()
    {
        aboutTheMazeText.SetActive(true);
        objectiveText.SetActive(false);
        controlsText.SetActive(false);
        chooseAnOptionText.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
