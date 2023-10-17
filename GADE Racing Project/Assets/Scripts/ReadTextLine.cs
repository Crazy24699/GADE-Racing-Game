using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadTextLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    private string filePath;
    private int currentLineIndex = 0;

    //private void Start()
    //{
    //    filePath = Application.dataPath + "/Dialog.txt";
    //    myText.text = GetLineAtIndex(currentLineIndex);
    //}

    //private string GetLineAtIndex(int index)
    //{
    //    string " ";
    //}
}
