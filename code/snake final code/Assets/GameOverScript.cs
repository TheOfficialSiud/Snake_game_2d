using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    
    public void restartButton()
    {
        //SoundManagerScript.PlaySound("gameover");
        SceneManager.LoadScene(0);
    }
   
}

