using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] footstepSounds;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayFootsteps()
    {
        AudioClip clip = footstepSounds[(int)Random.Range(0, footstepSounds.Length)];
        source.clip = clip;
        source.volume = Random.Range(0.02f, 0.05f);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }
}
