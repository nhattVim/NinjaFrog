using UnityEngine;
using Cinemachine;
using System.Collections;

public class LevelBoss : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    public CinemachineVirtualCamera virtualCamera;
    public float targetScreenY = 0.85f;
    public float transitionSpeed = 2f;
    public float targetOrthoSize = 8.2f;
    private CinemachineFramingTransposer framingTransposer;

    [Header("Boss")]
    public GameObject bossHPBar;
    public GameObject boss;
    public float delayBeforeBossAppears = 5f;

    [Header("Trophy")]
    public GameObject trophy;

    [Header("Background")]
    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    private float bgTime = 2;
    public float BGTime = 2;

    private bool bossDied = false;

    void Start()
    {
        if (virtualCamera != null)
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else
        {
            Debug.LogWarning("Virtual Camera is not assigned.");
        }


        if (bossHPBar != null)
            bossHPBar.SetActive(false);
        if (boss != null)
            boss.SetActive(false);
        if (trophy)
            trophy.SetActive(false);

        StartCoroutine(ActivateBossAfterDelay());
    }

    private IEnumerator ActivateBossAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeBossAppears);

        if (bossHPBar != null)
            bossHPBar.SetActive(true);
        if (boss != null)
            boss.SetActive(true);
    }

    void Update()
    {
        if (framingTransposer != null)
        {
            framingTransposer.m_ScreenY = Mathf.Lerp(framingTransposer.m_ScreenY, targetScreenY, Time.deltaTime * transitionSpeed);
        }

        if (virtualCamera != null)
        {
            float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, targetOrthoSize, Time.deltaTime * transitionSpeed);
        }

        if (boss != null && trophy != null && !bossDied)
        {
            BossJ97 bossScript = boss.GetComponent<BossJ97>();
            if (bossScript != null && bossScript.HP <= 0)
            {
                trophy.SetActive(true);
                bossDied = true;
            }
        }

        bgTime -= Time.deltaTime;
        if (bgTime <= 0)
        {
            int randomIndex = Random.Range(0, 3);
            switch (randomIndex)
            {
                case 0:
                    HideAllBG();
                    bg1.SetActive(true);
                    break;
                case 1:
                    HideAllBG();
                    bg2.SetActive(true);
                    break;
                case 2:
                    HideAllBG();
                    bg3.SetActive(true);
                    break;
            }
            bgTime = BGTime;
        }
    }

    private void HideAllBG()
    {
        bg1.SetActive(false);
        bg2.SetActive(false);
        bg3.SetActive(false);
    }
}
