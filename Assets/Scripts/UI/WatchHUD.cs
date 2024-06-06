using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WatchHUD : MonoBehaviour
{
    public Image progressImage;
    [SerializeField] private Image lightningBolt;
    [SerializeField] private float defaultSpeed;
    //[SerializeField] private UnityEvent<float> onProgress;
    [SerializeField] private UnityEvent onCompleted;

    private Coroutine animationCoroutine;

    private void Update()
    {
        if (animationCoroutine == null && progressImage.fillAmount < 1f)
        {
            SetProgress(1f);
        }
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
            //if (InputManager.isPaused) continue;
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
