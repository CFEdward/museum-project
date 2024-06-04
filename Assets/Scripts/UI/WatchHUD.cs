using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WatchHUD : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private float defaultSpeed;
    //[SerializeField] private UnityEvent<float> onProgress;
    [SerializeField] private UnityEvent onCompleted;

    private Coroutine animationCoroutine;

    public void SetProgress(float progress)
    {
        SetProgress(progress, defaultSpeed);
    }
    public void SetProgress(float progress, float speed)
    {
        if (progress < 0 || progress >1)
        {
            progress = Mathf.Clamp01(progress);
        }
        if (progress != progressImage.fillAmount)
        {
            if (animationCoroutine != null)
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

        progressImage.fillAmount = progress;
        //onProgress?.Invoke(progress);
        onCompleted?.Invoke();
    }
}
