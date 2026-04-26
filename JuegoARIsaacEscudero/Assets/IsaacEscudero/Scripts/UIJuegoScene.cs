using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class UIJuegoScene : MonoBehaviour
{
    [SerializeField] Canvas canvasJuego;
    UIForPreparingJuegoScene uIPrep;
    [SerializeField] TMP_Text textoTiempo;
    [SerializeField] TMP_Text textoGemas;
    float gemasPilladas = 0;
    float gemasTotales;
    public static UIJuegoScene instance;
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
        float minutes = Mathf.FloorToInt(tiempo / 60);
        float seconds = Mathf.FloorToInt(tiempo % 60);
        gemasTotales = gemasHorizontales + gemasVerticales;
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        textoGemas.text = gemasPilladas.ToString() + " / " + gemasTotales.ToString();
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
