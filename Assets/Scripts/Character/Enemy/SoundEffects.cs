using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip electrocutedSound;
    [SerializeField] private AudioClip dyingSound;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayElectrocutedSound()
    {
        source.clip = electrocutedSound;
        source.loop = true;
        source.volume = Random.Range(0.4f, 0.7f);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    public void PlayDyingSound()
    {
        source.clip = dyingSound;
        source.loop = true;
        source.volume = Random.Range(0.02f, 0.1f);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    public void StopElectrocutedSound()
    {
        source.loop = false;
        source.Stop();
    }

    public void StopDyingSound()
    {
        source.loop = false;
        source.Stop();
    }
}
