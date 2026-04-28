using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;


public class ChangeScenes : MonoBehaviour
{
    [SerializeField] int indiceNivelJuego;
    //Indice de la escena del menu de inicio
    [SerializeField] int indiceMenu;
    public static ChangeScenes player;
    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(player.gameObject);
        }
    }
    public void CambiarEscenaDeJuego()
    {
        ARSession arSession = FindFirstObjectByType<ARSession>();
        if (arSession != null)
        {
            arSession.Reset();
        }
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(arSession);
        SceneManager.LoadScene(indiceNivelJuego);
    }
    public void RegresarAlMenu()
    {
        //Se detienen todos los audioSources
        AudioManager.instance.StopAllAudioSources();
        //Y se carga la escena del menu de inicio
        ARSession arSession = FindFirstObjectByType<ARSession>();
        if (arSession != null)
        {
            arSession.Reset();
        }
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(arSession);
        SceneManager.LoadScene(indiceMenu);
    }
}
