using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class ConfigurationMenu : MonoBehaviour
{
    #region Atributos
    //Sliders y textos para los aspectos a configurar en el menu de inicio
    [SerializeField] Slider sliderHorizontalPlanes;
    [SerializeField] TMP_Text textoPlanosHorizontales;
    [SerializeField] Slider sliderVerticalPlanes;
    [SerializeField] TMP_Text textoPlanosVerticales;
    [SerializeField] Slider sliderTiempo;
    [SerializeField] TMP_Text textoTiempo;
    //Estas variables son estaticas para poder guardar datos al cambiar de escena
    //Estan tambien inicializadas por defecto para evitar problemas al pasar de una escena a otra
    static bool oclusionActivada = true;
    static float planosHorizontales = 1;
    static float planosVerticales = 1;
    static float tiempo = 60;
    //Es el indice de la escena del nivel del juego
    [SerializeField] int indiceNivelJuego;
    //Una referencia a este canvas
    Canvas canvas;
    #endregion
    #region Funciones de Unity
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        //Nos aseguramos de que el canvas solo se active en el menu de inicio
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
    #endregion
    #region Funciones para definir paramentros del menu
    //Funcion para definir cuantos planos horizontales se quiere
    public void HorizontalPlanesDetected(float hPlane)
    {
        //Se escribe por pantalla y se guarda el valor de los planos horizontales que se quiere
        textoPlanosHorizontales.text = "Numero de planos horizontales: " + hPlane;
        planosHorizontales = hPlane;
    }
    //Funcion para definir cuantos planos verticales se quiere
    public void VerticalPlanesDetected(float vPlane)
    {
        //Se escribe por pantalla y se guarda el valor de los planos verticales que se quiere
        textoPlanosVerticales.text = "Numero de planos verticales: " + vPlane;
        planosVerticales = vPlane;

    }
    //Funcion para definir el tiempo que se quiere
    public void DisplayTime(float time)
    {
        //Guardamos los minutos y los segundos
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        textoTiempo.text = "Tiempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        tiempo = time;
    }
    //Funcion para definir si se quiere la oclusion de imagen activada
    public void IsOclusionActivated(bool activado)
    {
        oclusionActivada = activado;
    }
    #endregion
    #region Cambio de escenas
    public void CambiarEscenaDeJuego()
    {
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(indiceNivelJuego);
    }
    #endregion
    #region Getters
    internal float GetPlanosHorizontalesMaximos()
    {
        return planosHorizontales;
    }
    internal float GetPlanosVerticalesMaximos()
    {
        return planosVerticales;
    }
    internal float GetTiempo()
    {
        return tiempo;
    }
    internal bool GetOclusion()
    {
        return oclusionActivada;
    }
    #endregion
}
