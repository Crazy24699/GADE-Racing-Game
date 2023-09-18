using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ReadTextLine : MonoBehaviour
{
    public TextAsset DialogFile;

    protected string FilePath;
    [SerializeField]protected string[] AllDialogLines;
    public TextMeshProUGUI Path;

    //Gives the needed amount of lines for the dialog to be displayed 
    protected int[] DialogLineValues =
    {
        2,
        2,
        2,
        10,
    };
        
    public int CurrentDialogLine;
    public int DialogLineCount;


    protected void Awake()
    {
        DialogLineCount = 0;
        CurrentDialogLine = 0;
        Debug.Log("run");
        FindDialogFile();
        
    }

    public void FindDialogFile()
    {
        DialogFile = Resources.Load<TextAsset>("Dialog");
        if (DialogFile != null)
        {
            //Application.Quit();
        }
        //FilePath = AssetDatabase.GetAssetPath(DialogFile);
        Debug.Log(DialogFile.text);

        AllDialogLines = DialogFile.text.Split('\n');
    }

    public Queue DialogLines()
    {
        Queue QueuedDialog=new Queue();
        //for (int i = DialogLineCount; i < DialogLineCount + DialogLineValues[DialogLineCount]; i++) 
        //{
        //    //QueuedDialog.Enqueue(AllDialogLines[i]);
        //    Debug.Log(CurrentDialogLine);
        //    CurrentDialogLine++;
        //}
        int DialogEndLine = CurrentDialogLine + DialogLineValues[DialogLineCount];
        for (; CurrentDialogLine < DialogEndLine;)
        {
            QueuedDialog.Enqueue(AllDialogLines[CurrentDialogLine]);

            //Debug.Log(AllDialogLines[CurrentDialogLine]);
            CurrentDialogLine++;
        }
        //CurrentDialogLine = CurrentDialogLine + DialogLineValues[DialogLineCount];
        DialogLineCount++;

        Debug.Log(QueuedDialog.Count);

        return QueuedDialog;
    }


    
}
