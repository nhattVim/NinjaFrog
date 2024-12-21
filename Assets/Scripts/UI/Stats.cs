using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameObject groupStats;
    private bool isStats;





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (isStats)
            {

                turnOffStats();
            }
            else

                turnOnStats();
        }


    }


    void turnOnStats()
    {
        isStats = true;
        groupStats.SetActive(true);


    }
    void turnOffStats()
    {
        isStats = false;
        groupStats.SetActive(false);

    }






}
