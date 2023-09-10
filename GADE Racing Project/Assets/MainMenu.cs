using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{


    public List<GameObject> PanelList;
    [SerializeField]protected ReadTextLine ReadDialogScript;
    public TextMeshProUGUI DialogText;

    //Counts how many times the player has clicked the next dialog button 
    public int DialogCount = 0;

    public void StartGame()
    {
        ReadDialogScript = GameObject.FindAnyObjectByType<ReadTextLine>();
        GameObject DialogTextboxRef = GameObject.Find("DialogTextbox");
        DialogText = DialogTextboxRef.GetComponent<TextMeshProUGUI>();


    }


    public void MainMenuActive()
    {
        EnablePanel("Main Menu");
    }

    public void SelectDifficultyPanel()
    {
        EnablePanel("Difficulty Selector");
    }

    public void DialogMenu()
    {
        EnablePanel("Dialog Menu");

        if(DialogCount < 3)
        {
            NextDialogPortion();
        }
        else
        {
            SelectDifficultyPanel();
            GameObject DialogTextboxRef = GameObject.Find("DifficultyTextBox");
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
            if(!UIPanel.name.Contains(ActivePanel))
            {
                UIPanel.SetActive(false);
                return;
            }
            UIPanel.SetActive(true);
        }
    }

}
