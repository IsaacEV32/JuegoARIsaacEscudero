using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Audio Sources donde saldran los clips
    [SerializeField] AudioSource audioSourceBackgroundMusic;
    [SerializeField] AudioSource audioSourceSFX;
    //Los clips a reproducir
    [SerializeField] AudioClip audioClipBackgroundMusic;
    [SerializeField] AudioClip audioClipPickSound;
    //Variable para acceder a la UI de preparacion de juego
    UIForPreparingJuegoScene uIPrep;
    //Variable usada para singelton
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
        //Si no estamos en la escena de juego y la UI de preparacion de juego esta desactivada
        // o si  estamos en la escena de juego y la UI de preparacion de juego esta activada
        if (SceneManager.GetActiveScene().buildIndex != 1 && !uIPrep.GetCanvasPrepararJuego().enabled
            || SceneManager.GetActiveScene().buildIndex == 1 && uIPrep.GetCanvasPrepararJuego().enabled)
        {
            //Se desactiva este script
            this.enabled = false;
        }
    }
    //Funciones para reproducir musica y efectos de sonido
    internal void PlayBackgroundMusic()
    {
        audioSourceBackgroundMusic.PlayOneShot(audioClipBackgroundMusic);
    }
    internal void PlayPickSoundEffect()
    {
        audioSourceSFX.PlayOneShot(audioClipPickSound);
    }
    //Funcion para parar todos los sonidos
    internal void StopAllAudioSources()
    {
        audioSourceBackgroundMusic.Stop();
        audioSourceSFX.Stop();
    }
}
