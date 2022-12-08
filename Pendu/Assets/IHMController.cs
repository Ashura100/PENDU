using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IHMController : MonoBehaviour 
{

    [SerializeField]
    private GameObject errorPanel;
    [SerializeField]
    private Text textError;

    [SerializeField]
    private Text textInfo;

    [SerializeField]
    private Text textAction;

    [SerializeField]
    private TMP_InputField userInput;

    [SerializeField]
    private Text debugInfo;

    [SerializeField]
    private Text SaveLetter;

    [SerializeField]
    private Text GuessedLetter;

    [SerializeField]
    private Image Img;

    public bool debugOn;

    //objet GAME pour gérer le traitement
    public GameManager gameManager;
    public GameObject actionPanelParent;
    public GameObject playerChoiceParent;
    // Use this for initialization
    void Start ()
    {
        gameManager = GetComponent<GameManager>();
	}
	void Update () 
    {
		
        //only in debug mode
        if(debugOn){
            if(Input.GetKeyDown(KeyCode.E)){
                showErrorMessage("ERROR : ceci est un messge de dev", Color.yellow);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                showErrorMessage("il vous reste 6 essais - indice : $", Color.grey);
            }
        }
	}
    public List<Sprite> spritesList;

    public void UpdateHangmanSprite(int nombreMax, int vieRestante)
    {
        int index = nombreMax - vieRestante;
        Sprite monSprite = spritesList[index];
        Img.sprite = monSprite;

    }

    public void showErrorMessage(string message,Color color)
    {
        errorPanel.SetActive(true);
        textError.color = color;
        textError.text = message;
        StartCoroutine(waitAndClose(errorPanel));
    }

    IEnumerator waitAndClose(GameObject panelToClose)
    {
        yield return new WaitForSeconds(4f);
        panelToClose.SetActive(false);
    }
    public void DisplayAndClear(Text textField, string message)
    {
        StartCoroutine(DisplayAndClearCorout(textField, message));
    }
    IEnumerator DisplayAndClearCorout(Text textField, string message)
    {
        textField.text = message;
        yield return new WaitForSeconds(2f);
        textField.text = "";
    }

    public void showInfoMessage(string message)
    {
        textInfo.text = message;
       
    }

    public void showActionMessage(string message)
    {
        DisplayAndClear(textAction, message);
    }

    public void toggleDebugInfo(bool state,string message)
    {
        debugInfo.gameObject.SetActive(state);
        debugInfo.text = message;
    }

    public void UpdateSaveLetter()
    {
        string result = "";
        foreach (char letter in gameManager.currentGame.playLetterList) 
        {
            result += letter+",";
        }
        SaveLetter.text = result;
    }

    public void DisplayGuessed()
    {
        string result = "";
        for (int i = 0; i < gameManager.currentGame.word.Length; i++)
        {
            if (!gameManager.currentGame.playLetterList.Contains(gameManager.currentGame.word[i]))
            {
                result += "_ ";
            }
            else
            {
                result += gameManager.currentGame.word[i];
            }
        }
        GuessedLetter.text = result;
    }
    public void setInputFieldText(string message)
    {
        userInput.text = message;
        //userInput.GetComponent<Text>().text = message;
    }

    //called by OK button
    public void OnUserInputFieldValidate()
    {

        string inputValue = userInput.text;
        Debug.Log("InputField value : " + inputValue);
        gameManager.currentGame.CheckUserWord(inputValue);
    }
}
