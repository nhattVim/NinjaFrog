using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (pointText != null && player != null)
        {
            pointText.text = player.numOfCherries.ToString();
        }
    }
}
