using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class HPPotion : MonoBehaviour
{
    public float HP;
    private GameObject playerObject;
    private Player playerScript;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Variables.Object(playerObject).Set("currentHP", playerScript.currentHP += HP);
            GetComponent<Animator>().SetTrigger("Collected");
            StartCoroutine(DelayDestroy());
        }
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
