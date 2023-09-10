using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public List<GameObject> PanelList;
    [SerializeField]protected ReadTextLine ReadDialogScript;
    public TextMeshProUGUI DialogText;

    //Counts how many times the player has clicked the next dialog button 
    public int DialogCount = 0;

    public void StartGame()
    {
        EnablePanel("Dialog Menu");
        ReadDialogScript = GameObject.FindAnyObjectByType<ReadTextLine>();
        GameObject DialogTextboxRef = GameObject.Find("DialogTextbox");
        DialogText = DialogTextboxRef.GetComponent<TextMeshProUGUI>();
        
    }

    public void MainMenuActive()
    {
        EnablePanel("Main Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SelectDifficultyPanel()
    {
        EnablePanel("Difficulty Selector");
    }

    public void DialogMenu()
    {
        

        if(DialogCount < 3)
        {
            NextDialogPortion();
            
        }
        if(DialogCount >= 3) 
        {

            Debug.Log("the snoiw");
            SelectDifficultyPanel();
            GameObject DialogTextboxRef = GameObject.Find("DifficultyTextbox");
            DialogText = DialogTextboxRef.GetComponent<TextMeshProUGUI>();
            NextDialogPortion();
        }
    }

    public void NextDialogPortion()
    {
        DialogText.ClearMesh();

        if(ReadDialogScript == null)
        {
            ReadDialogScript = GameObject.FindAnyObjectByType<ReadTextLine>();
        }

        Queue DialogQueue = ReadDialogScript.DialogLines();
        string[] Lines = DialogQueue.ConvertTo<string[]>();
        string WrittenDialog = "";
        foreach(string Line in DialogQueue)
        {
            WrittenDialog = WrittenDialog + Line + "\n";
        }
        DialogCount++;
        DialogText.text = WrittenDialog;
    }

    public void EnablePanel(string ActivePanel)
    {
        
        foreach (GameObject UIPanel in PanelList)
        {
            UIPanel.SetActive(false);
            
            if(UIPanel.name.Contains(ActivePanel))
            {
                UIPanel.SetActive(true);
                
            }
            Debug.Log(UIPanel.name);
        }
    }

    public void EasyModeSelected()
    {
        SceneManager.LoadScene("Testing Scene");
    }

    public void MoveToMainMenu()
    {
        EnablePanel("Main Menu");
    }

}
