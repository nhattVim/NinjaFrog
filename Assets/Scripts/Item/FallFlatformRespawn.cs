using UnityEngine;
using System.Collections;

public class FallFlatformRespawn : MonoBehaviour
{
    public GameObject fallFlatForm;
    private Vector3 initPosition;
    private bool isRespawning = false;

    void Start()
    {
        initPosition = fallFlatForm.transform.position;
    }

    void Update()
    {
        FallFloatform fallScript = fallFlatForm.GetComponent<FallFloatform>();
        if (fallScript != null && fallScript.IsDestroyed && !isRespawning)
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    private IEnumerator FallAndRespawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(0.5f);
        fallFlatForm.transform.position = initPosition;
        fallFlatForm.SetActive(true);
        fallFlatForm.GetComponent<FallFloatform>().ResetPlatform();
        isRespawning = false;
    }
}
