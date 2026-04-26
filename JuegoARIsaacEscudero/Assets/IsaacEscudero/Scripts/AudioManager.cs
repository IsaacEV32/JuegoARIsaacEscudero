using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceBackgroundMusic;
    [SerializeField] AudioSource audioSourceSFX;
    [SerializeField] AudioClip audioClipBackgroundMusic;
    [SerializeField] AudioClip audioClipPickSound;
    UIForPreparingJuegoScene uIPrep;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        uIPrep = GetComponentInParent<UIForPreparingJuegoScene>();
        if (SceneManager.GetActiveScene().buildIndex != 1 && !uIPrep.GetCanvasPrepararJuego().enabled
            || SceneManager.GetActiveScene().buildIndex == 1 && uIPrep.GetCanvasPrepararJuego().enabled)
        {
            this.enabled = false;
        }
    }
    internal void PlayBackgroundMusic()
    {
        audioSourceBackgroundMusic.PlayOneShot(audioClipBackgroundMusic);
    }
    internal void PlayPickSoundEffect()
    {
        audioSourceSFX.PlayOneShot(audioClipPickSound);
    }
    internal void StopAllAudioSources()
    {
        audioSourceBackgroundMusic.Stop();
        audioSourceSFX.Stop();
    }
}
