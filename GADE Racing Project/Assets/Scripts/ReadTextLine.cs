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

    //Gives the needed amount of lines for the dialog to be displayed 
    protected int[] DialogLineValues =
    {
        2,
        2,
        2,
        10,
    };
        
    [SerializeField]protected int CurrentDialogLine;
    [SerializeField]protected int DialogLineCount;


    protected void Awake()
    {
        DialogLineCount = 0;
        Debug.Log("run");
        FindDialogFile();
        
    }

    protected void FindDialogFile()
    {
        DialogFile = Resources.Load<TextAsset>("Dialog");
        FilePath = AssetDatabase.GetAssetPath(DialogFile);
        AllDialogLines = File.ReadAllLines(FilePath);
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
