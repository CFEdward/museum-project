using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip electrocutedSound;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayElectrocutedSound()
    {
        source.clip = electrocutedSound;
        source.loop = true;
        source.volume = Random.Range(0.2f, 0.5f);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    public void StopElectrocutedSound()
    {
        source.loop = false;
        source.Stop();
    }
}
