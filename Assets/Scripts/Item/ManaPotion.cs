using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ManaPotion : MonoBehaviour
{
    public float Mana;
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
            Variables.Object(playerObject).Set("currentMana", playerScript.currentMana += Mana);
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
