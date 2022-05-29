using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    private void OnEnable() {
        GameManager.restartGameEvent += RestartBGM;
    }

    private void OnDisable() {
        GameManager.restartGameEvent -= RestartBGM;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void RestartBGM()
    {
        bgmSource.Stop();
        bgmSource.Play();
    }
}
