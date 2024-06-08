using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WatchHUD : MonoBehaviour, IDataPersistence
{
    public Image progressImage;
    [SerializeField] private Image lightningBolt;
    [SerializeField] private float defaultSpeed;
    //[SerializeField] private UnityEvent<float> onProgress;
    [SerializeField] private UnityEvent onCompleted;

    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;

    [SerializeField] private GameObject detectionInvestigate;
    [SerializeField] private GameObject detectionAlerted;

    private Coroutine animationCoroutine;

    private void Update()
    {
        if (animationCoroutine == null && progressImage.fillAmount < 1f)
        {
            SetProgress(1f);
        }
        switch (PlayerData.livesLeft)
        {
            case 0:
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                break;
            case 1:
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                break;
            case 2:
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                break;
            case 3:
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Cannot set lives HUD");
                break;
        }

        CheckDetection();
    }

    private void CheckDetection()
    {
        if (PlayerData.bIsPursued)
            detectionAlerted.SetActive(true);
        else detectionAlerted.SetActive(false);

        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        for (int enemyIndex = 0; enemyIndex < enemies.Length; enemyIndex++)
        {
            EnemyManager enemy = enemies[enemyIndex];
            if (enemy.alertStage == AlertStage.Intrigued && enemy.alertStage != AlertStage.Alerted)
            {
                detectionInvestigate.SetActive(true);
                break;
            }
            else if (enemies[enemies.Length - 1].alertStage != AlertStage.Intrigued)
            {
                detectionInvestigate.SetActive(false);
            }
        }
    }

    public void LoadData(GameData data)
    {
        this.progressImage.fillAmount = data.stunBarFillAmount;
        this.lightningBolt.enabled = data.hudLightningEnabled;
    }

    public void SaveData(GameData data)
    {
        data.stunBarFillAmount = this.progressImage.fillAmount;
        data.hudLightningEnabled = this.lightningBolt.enabled;
    }

    public void ResetCooldown()
    {
        progressImage.fillAmount = 0f;
        lightningBolt.enabled = false;
        animationCoroutine = StartCoroutine(AnimateProgress(1f, defaultSpeed));
    }

    public void SetProgress(float progress)
    {
        SetProgress(progress, defaultSpeed);
    }
    public void SetProgress(float progress, float speed)
    {
        if (progress < 0f || progress > 1f)
        {
            progress = Mathf.Clamp01(progress);
        }
        if (progress != progressImage.fillAmount)
        {
            if (animationCoroutine != null && progressImage.fillAmount >= 1f)
            {
                StopCoroutine(animationCoroutine);
            }

            animationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }
    }

    private IEnumerator AnimateProgress(float progress, float speed)
    {
        float time = 0f;
        float initialProgress = progressImage.fillAmount;

        while (time < 1f)
        {
            progressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
            time += Time.deltaTime * speed;

            //onProgress?.Invoke(progressImage.fillAmount);
            yield return null;
        }

        lightningBolt.enabled = true;
        progressImage.fillAmount = progress;
        //onProgress?.Invoke(progress);
        onCompleted?.Invoke();
    }
}
