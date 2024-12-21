using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void exitOnGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Game exit");
    }

    public void onStartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void continueGame()
    {
        // code cho tiếp tục game
    }
}
