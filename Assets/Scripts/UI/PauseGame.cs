using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel; // Panel dùng để hiển thị menu tạm dừng
    private bool isPaused = false; // Trạng thái tạm dừng

    void Update()
    {
        // Kiểm tra nếu người dùng nhấn phím ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Tiếp tục trò chơi
            }
            else
            {
                Pause(); // Tạm dừng trò chơi
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Hiển thị Panel
        Time.timeScale = 0f; // Dừng thời gian trong game
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Ẩn Panel
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
    }
    
}
