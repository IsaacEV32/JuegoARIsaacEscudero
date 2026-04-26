using TMPro;
using Unity.VisualScripting;
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
    static bool oclusionActivada;
    static float planosHorizontales;
    static float planosVerticales;
    static float tiempo;
    [SerializeField] int indiceNivelJuego;
    Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderHorizontalPlanes.onValueChanged.AddListener((v) => { textoPlanosHorizontales.text = v.ToString("Numero de planos horizontales: " + " 0"); });
        sliderVerticalPlanes.onValueChanged.AddListener((v) => { textoPlanosVerticales.text = v.ToString("Numero de planos verticales: " + " 0"); });
    }
    public void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
    // Update is called once per frame
    void Update()
    {

    }
}
