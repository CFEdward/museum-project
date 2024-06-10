using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    [SerializeField] private float fadeRate;
    [SerializeField] private GameObject[] screens;
    [SerializeField] private Image[] images;
    private BackgroundMusic bgMusic;
    private int imageToFadeIndex = 0;
    private float targetAlpha = 1f;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        bgMusic = FindObjectOfType<BackgroundMusic>();
    }

    private void Start()
    {
        inputManager.Interacted += StartNextImage;
        foreach (Image image in images)
        {
            Color currentColor = image.color;
            currentColor.a = 0f;
            image.color = currentColor;
        }
        StartNextImage();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void StartNextImage()
    {
        switch (imageToFadeIndex)
        {
            case 0:
                StartCoroutine(FadeIn(images[imageToFadeIndex]));
                break;
            case 2:
                screens[0].SetActive(false);
                screens[1].SetActive(true);
                break;
            case 3:
                screens[1].SetActive(false);
                screens[2].SetActive(true);
                break;
            case 4:
                bgMusic.StartCoroutine(bgMusic.Fade(false, bgMusic.source, 1f, 0f));
                SceneManager.LoadSceneAsync(0);
                return;
            default:
                break;
        }
        Image imageToFade = images[imageToFadeIndex];
        imageToFadeIndex++;
        StartCoroutine(FadeIn(imageToFade));
    }

    private IEnumerator FadeIn(Image image)
    {
        Color currentColor = image.color;
        while (Mathf.Abs(currentColor.a - targetAlpha) > 0.0001f)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, fadeRate * Time.deltaTime);
            image.color = currentColor;
            yield return null;
        }
    }

    private void OnDisable()
    {
        inputManager.Interacted -= StartNextImage;
    }
}
