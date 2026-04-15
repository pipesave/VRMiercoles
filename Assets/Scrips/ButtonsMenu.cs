using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonsMenu : MonoBehaviour
{
    
    public void BotonPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
