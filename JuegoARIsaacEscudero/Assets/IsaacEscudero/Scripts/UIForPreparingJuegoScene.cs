using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class UIForPreparingJuegoScene : MonoBehaviour
{
    #region Atributos
    //Referencia al manager de planos
    ARPlaneManager aRPlaneManager;
    //Referencia al menu de configuracion
    ConfigurationMenu configurationMenu;
    //Referencia a la UI del juego
    UIJuegoScene juegoGemas;
    //Referencia al script de generar las gemas
    GenerarGemas generarGemas;
    //Textos para cada tipo de plano
    [SerializeField] TMP_Text textForHorizontalPlanesDetected;
    [SerializeField] TMP_Text textForVerticalPlanesDetected;
    //Referencia a este canvas
    [SerializeField] Canvas canvas;
    //Boton para poder iniciar partida
    [SerializeField] Button botonParaJugar;
    //Variables para contar cuantos planos de cada tipo se han añadido en tiempo real
    int horizontalPlanes;
    int verticalPlanes;
    //Variables donde se guardaran los valores que se guardaron en el menu de configuracion
    float horizontalPlanesMin;
    float verticalPlanesMin;
    float tiempoJuego;
    bool estaLaOclusionActivada;
    //Referencia al Manager de la oclusion de imagenes AR
    [SerializeField] AROcclusionManager aROcclusionManager;
    #endregion
    #region Metodos de Unity
    private void Awake()
    {
        //Conseguimos los componentes
        aRPlaneManager = GetComponent<ARPlaneManager>();
        configurationMenu = GetComponentInChildren<ConfigurationMenu>();
        juegoGemas = GetComponent<UIJuegoScene>();
        generarGemas = GetComponentInChildren<GenerarGemas>();
        //Conseguimos los parametros de la escena del menu
        horizontalPlanesMin = configurationMenu.GetPlanosHorizontalesMaximos();
        verticalPlanesMin = configurationMenu.GetPlanosVerticalesMaximos();
        tiempoJuego = configurationMenu.GetTiempo();
        estaLaOclusionActivada = configurationMenu.GetOclusion();
        //Desactivamos o activamos la oclusion de imagenes
        if (estaLaOclusionActivada)
        {
            aROcclusionManager.enabled = true;
        }
        else
        {
            aROcclusionManager.enabled = false;
        }
        //Desactivamos el boton de jugar al inicio
        botonParaJugar.gameObject.SetActive(false);
        //Comprobamos que estemos en la escena de juego para activar o desactivar este canvas y el manager de planos AR
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
    #endregion
    #region Metodos
    //Usamos esta funcion en el TrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> arPlanes) para detectar cuando se cambian los planos
    public void PlanesDetected(ARTrackablesChangedEventArgs<ARPlane> arPlanes)
    {
        //Se reinician los planos horizontales y verticales para reiniciar los contadores
        horizontalPlanes = 0;
        verticalPlanes = 0;
        //Se comprueba por cada plano de la lista de trackables del manager de planos AR
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            //Solo contamos los planos que estan siendo rastreados y son horizontales
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.HorizontalUp) || plane.alignment == PlaneAlignment.HorizontalDown)
            {
                horizontalPlanes++;
            }
            //Solo contamos los planos que estan siendo rastreados y son verticales
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.Vertical))
            {
                verticalPlanes++;
            }
        }
        //Se muestran por pantalla el numero de planos detectados y el numero de planos necesarios para cada tipo
        textForHorizontalPlanesDetected.text = "Planos horizontales: " + " " + horizontalPlanes + " / " + horizontalPlanesMin;
        textForVerticalPlanesDetected.text = "Planos verticales: " + " " + verticalPlanes + " / " + verticalPlanesMin;
        //Se activa el boton de jugar si se superan o igualan el numero de planos detectados y el numero de planos necesarios,
        //tanto horizontales como verticales
        if (horizontalPlanes >= horizontalPlanesMin && verticalPlanes >= verticalPlanesMin)
        {
            botonParaJugar.gameObject.SetActive(true);
        }
        //Se desactiva si no se cumple alguna de las condiciones anteriores
        else if (horizontalPlanes < horizontalPlanesMin || verticalPlanes < verticalPlanesMin)
        {
            botonParaJugar.gameObject.SetActive(false);
        }
    }
    //Funcion usada para empezar el juego
    public void Jugar()
    {
        //Se desactiva este canvas
        canvas.enabled = false;
        //Se activa el UI del juego
        juegoGemas.GetJuegoGemasCanvas().enabled = true;
        //Se establecen todos los parametros para empezar a jugar
        juegoGemas.SetParametrosEnJuegoGemas(horizontalPlanesMin, verticalPlanesMin, tiempoJuego);
        //Se generan las gemas
        generarGemas.SetGemasInScene();
        //Se activa el audio manager y se reproduce la musica de fondo
        AudioManager.instance.enabled = true;
        AudioManager.instance.PlayBackgroundMusic();
    }
    #endregion
    #region Getters
    //Se usa para conseguir este canvas
    internal Canvas GetCanvasPrepararJuego()
    {
        return canvas;
    }
    #endregion
}
