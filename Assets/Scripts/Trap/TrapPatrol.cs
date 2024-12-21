using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPatrol : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed;
    public float atk = 5f;
    private float lastDamageTime;
    public float damageDelay = 0.1f;
    private int currentsWayPointsIndex;

    void Update()
    {
        if (Vector2.Distance(wayPoints[currentsWayPointsIndex].position, transform.position) < 0.1f)
        {
            currentsWayPointsIndex++;
            if (currentsWayPointsIndex >= wayPoints.Length)
            {
                currentsWayPointsIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentsWayPointsIndex].position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                coll.gameObject.GetComponent<Player>()?.TakeDamage(atk);
                Debug.Log("Trap gây sát thương: " + atk);
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                coll.GetComponent<Player>()?.TakeDamage(atk);
                Debug.Log("Trap gây sát thương: " + atk);
                lastDamageTime = Time.time;
            }
        }
    }
}
