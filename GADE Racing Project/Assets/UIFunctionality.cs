using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctionality : MonoBehaviour
{
    public void Start()
    {
        
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
