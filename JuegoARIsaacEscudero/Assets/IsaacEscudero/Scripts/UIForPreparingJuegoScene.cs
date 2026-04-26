using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class UIForPreparingJuegoScene : MonoBehaviour
{
    ARPlaneManager aRPlaneManager;
    ConfigurationMenu configurationMenu;
    UIJuegoScene juegoGemas;
    GenerarGemas generarGemas;
    [SerializeField] TMP_Text textForHorizontalPlanesDetected;
    [SerializeField] TMP_Text textForVerticalPlanesDetected;
    [SerializeField] Canvas canvas;
    [SerializeField] Button botonParaJugar;
    int horizontalPlanes;
    int verticalPlanes;
    float horizontalPlanesMin;
    float verticalPlanesMin;
    float tiempoJuego;
    bool estaLaOclusionActivada;
    [SerializeField] AROcclusionManager aROcclusionManager;
    private void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        configurationMenu = GetComponentInChildren<ConfigurationMenu>();
        juegoGemas = GetComponent<UIJuegoScene>();
        generarGemas = GetComponentInChildren<GenerarGemas>();
        horizontalPlanesMin = configurationMenu.GetPlanosHorizontalesMaximos();
        verticalPlanesMin = configurationMenu.GetPlanosVerticalesMaximos();
        tiempoJuego = configurationMenu.GetTiempo();
        estaLaOclusionActivada = configurationMenu.GetOclusion();
        if (estaLaOclusionActivada)
        {
            aROcclusionManager.enabled = true;
        }
        else
        {
            aROcclusionManager.enabled = false;
        }
        botonParaJugar.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            canvas.enabled = false;
            aRPlaneManager.enabled = false;
        }
        else
        {
            canvas.enabled = true;
            aRPlaneManager.enabled = true;
        }
    }
    public void PlanesDetected(ARTrackablesChangedEventArgs<ARPlane> arPlanes)
    {
        horizontalPlanes = 0;
        verticalPlanes = 0;
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            // Solo contamos los que están siendo rastreados y son horizontales
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.HorizontalUp))
            {
                horizontalPlanes++;
            }
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.Vertical))
            {
                verticalPlanes++;
            }
        }
        textForHorizontalPlanesDetected.text = "Planos horizontales: " + " " + horizontalPlanes + " / " + horizontalPlanesMin;
        textForVerticalPlanesDetected.text = "Planos verticales: " + " " + verticalPlanes + " / " + verticalPlanesMin;
        if (horizontalPlanes >= horizontalPlanesMin && verticalPlanes >= verticalPlanesMin)
        {
            botonParaJugar.gameObject.SetActive(true);
        }
        else if (horizontalPlanes < horizontalPlanesMin || verticalPlanes < verticalPlanesMin)
        {
            botonParaJugar.gameObject.SetActive(false);
        }
    }
    public void Jugar()
    {
        canvas.enabled = false;
        juegoGemas.GetJuegoGemasCanvas().enabled = true;
        juegoGemas.SetParametrosEnJuegoGemas(horizontalPlanesMin, verticalPlanesMin, tiempoJuego);
        generarGemas.SetGemasInScene();
    }
    internal Canvas GetCanvasPrepararJuego()
    {
        return canvas;
    }
}
