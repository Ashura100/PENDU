using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Pendu
{
    public string word;
    public List<string> wordList = new List<string>() { "explosion", "destruction", "deflagration", "programmation", "feinté" };
    public List<char> playLetterList = new List<char>();
    int counter = 0;
    int nombreMax = 11;
    bool partieGagnee 
    {
        get
        {
            for (int i = 0; i < word.Length; i++) 
            {
               if (!playLetterList.Contains(word[i]))
               {
                    return false;
               }
            }
            return true;
        }
    }
    GameManager gameManager;

    public Pendu(GameManager gameManager)
    {
        this.gameManager = gameManager;
        word = GetRandomWord();
    }
    string GetRandomWord()
    {
        System.Random rnd = new System.Random();
        //Console.WriteLine("le mot à deviner "+rndIndex);
        int rndIndex = rnd.Next(0, wordList.Count);
        return wordList[rndIndex].ToUpper();
    }
    public void CheckUserWord(string inputValue)
    {
        if (!IsInputValid(inputValue))
        {
            return;
        }
        
        Debug.Log(word + " " + inputValue);
        gameManager.IHM.showInfoMessage($"il vous reste {nombreMax - counter} essaies");
        gameManager.IHM.showActionMessage("veuillez saisir une lettre");
        Debug.Log($"veuillez saisir une lettre, il vous reste {nombreMax - counter} essaies");
        inputValue = inputValue.ToUpper();
        char letter = inputValue[0];

        if (!playLetterList.Contains(letter))
        {
            playLetterList.Add(letter);
            bool isInWord = word.Contains(letter);
            if (isInWord)
            {
                gameManager.IHM.showErrorMessage($"vous avez une lettre : {letter}", Color.green);
            }
            else
            {
                gameManager.IHM.showErrorMessage($"cette lettre : {letter} n'est pas dans le mot", Color.black);
                gameManager.IHM.setInputFieldText("");
                counter++;
                gameManager.IHM.UpdateHangmanSprite(nombreMax, nombreMax - counter);
            }
        }
        else
        {
            gameManager.IHM.showErrorMessage($"vous avez déjà joué la lettre : {letter}", Color.red);
            counter++;
            gameManager.IHM.UpdateHangmanSprite(nombreMax, nombreMax - counter);

        }


        if (partieGagnee)
        {
  
            gameManager.IHM.showErrorMessage("vous avez gagnée", Color.yellow);
            gameManager.IHM.actionPanelParent.SetActive(false);
            gameManager.IHM.playerChoiceParent.SetActive(true);
        }


        if ((nombreMax - counter) <= 0)
        {
            gameManager.IHM.showErrorMessage("GAME OVER !", Color.red);
            gameManager.IHM.actionPanelParent.SetActive(false);
            gameManager.IHM.playerChoiceParent.SetActive(true);

        }
        
        gameManager.IHM.UpdateSaveLetter();
        gameManager.IHM.DisplayGuessed();
    }

    bool IsInputValid(string input)
    {
        if (input.Length == 0 || !Char.IsLetter(input[0]))
        {
            gameManager.IHM.showActionMessage("veuillez saisir une lettre");
            return false;
        }
        return true;
    }
}