using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Thoát game trong Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Thoát game khi chạy bản build
        Application.Quit();
        #endif
    }
}
