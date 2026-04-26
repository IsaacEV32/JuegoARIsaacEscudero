using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class UIJuegoScene : MonoBehaviour
{
    [SerializeField] Canvas canvasJuego;
    UIForPreparingJuegoScene uIPrep;
    [SerializeField] TMP_Text textoTiempo;
    [SerializeField] TMP_Text textoGemas;
    [SerializeField] TMP_Text textoFinalDelJuego;
    float gemasPilladas = 0;
    float gemasTotales;
    public static UIJuegoScene instance;
    bool chronometerActive = false;
    float tiempo;
    [SerializeField] Button botonVolverAlMenu;
    [SerializeField] int indiceMenu;
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        uIPrep = GetComponent<UIForPreparingJuegoScene>();
        if (SceneManager.GetActiveScene().buildIndex != 1 && !uIPrep.GetCanvasPrepararJuego().enabled
            || SceneManager.GetActiveScene().buildIndex == 1 && uIPrep.GetCanvasPrepararJuego().enabled)
        {
            canvasJuego.enabled = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && !uIPrep.GetCanvasPrepararJuego().enabled)
        {
            canvasJuego.enabled = true;
        }
    }
    internal void SetParametrosEnJuegoGemas(float gemasHorizontales, float gemasVerticales, float tiempo)
    {
        this.tiempo = tiempo;
        float minutes = Mathf.FloorToInt(tiempo / 60);
        float seconds = Mathf.FloorToInt(tiempo % 60);
        gemasTotales = gemasHorizontales + gemasVerticales;
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        textoGemas.text = gemasPilladas.ToString() + " / " + gemasTotales.ToString();
        chronometerActive = true;
        botonVolverAlMenu.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (chronometerActive)
        {
            if (gemasPilladas >= gemasTotales)
            {
                chronometerActive = false;
                textoFinalDelJuego.text = "Felicidades, ganaste la partida jugador";
                botonVolverAlMenu.gameObject.SetActive(true);
            }
            else if (tiempo >= 0)
            {
                tiempo -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(tiempo / 60);
                float seconds = Mathf.FloorToInt(tiempo % 60);
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else if (tiempo < 0)
            {
                tiempo = 0;
                float minutes = Mathf.FloorToInt(tiempo / 60);
                float seconds = Mathf.FloorToInt(tiempo % 60);
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                chronometerActive = false;
                textoFinalDelJuego.text = "Mala suerte jugador, perdiste";
                botonVolverAlMenu.gameObject.SetActive(true);
            }
        }
    }
    public void RegresarAlMenu()
    {
        AudioManager.instance.StopAllAudioSources();
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(indiceMenu);
    }
    internal Canvas GetJuegoGemasCanvas()
    {
        return canvasJuego;
    }
    internal void ActualizarGemasPilladas()
    {
        gemasPilladas++;
        textoGemas.text = gemasPilladas.ToString() + " / " + gemasTotales.ToString();
    }
    internal float GetGemasTotales()
    {
        return gemasTotales;
    }
}
