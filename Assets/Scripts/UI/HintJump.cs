using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintJump : MonoBehaviour
{
    public GameObject pannelGuide;
    private bool pauseGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pannelGuide.SetActive(true);
            Time.timeScale = 0f;
            pauseGame = true;
        }
    }

    private void Update()
    {
        if (pauseGame && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            pannelGuide.SetActive(false);
            Destroy(gameObject);
        }
    }
}
