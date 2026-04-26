using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class ConfigurationMenu : MonoBehaviour
{
    [SerializeField] Slider sliderHorizontalPlanes;
    [SerializeField] TMP_Text textoPlanosHorizontales;
    [SerializeField] Slider sliderVerticalPlanes;
    [SerializeField] TMP_Text textoPlanosVerticales;
    [SerializeField] Slider sliderTiempo;
    [SerializeField] TMP_Text textoTiempo;
    static bool oclusionActivada = true;
    static float planosHorizontales;
    static float planosVerticales;
    static float tiempo;
    [SerializeField] int indiceNivelJuego;
    Canvas canvas;
    [SerializeField] AROcclusionManager aROcclusionManager;
    private void Awake()
    {
        if (oclusionActivada)
        {
            aROcclusionManager.enabled = true;
        }
        else
        {
            aROcclusionManager.enabled = false;
        }
        canvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
    public void HorizontalPlanesDetected(float hPlane)
    {
        textoPlanosHorizontales.text = "Numero de planos horizontales: " + hPlane;
        planosHorizontales = hPlane;
    }
    public void VerticalPlanesDetected(float vPlane)
    {
        textoPlanosVerticales.text = "Numero de planos horizontales: " + vPlane;
        planosVerticales = vPlane;
    }
    public void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        tiempo = time;
    }
    public void IsOclusionActivated(bool activado)
    {
        oclusionActivada = activado;
    }

    public void CambiarEscenaDeJuego()
    {
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(indiceNivelJuego);
    }
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
