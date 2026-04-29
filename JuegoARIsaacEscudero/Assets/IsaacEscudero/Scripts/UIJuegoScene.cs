using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class UIJuegoScene : MonoBehaviour
{
    #region Atributos
    //Referencia a este canvas
    [SerializeField] Canvas canvasJuego;
    //Referencia a la UI de preparar el juego
    UIForPreparingJuegoScene uIPrep;
    //Textos para mostrar en pantalla
    [SerializeField] TMP_Text textoTiempo;
    [SerializeField] TMP_Text textoGemas;
    [SerializeField] TMP_Text textoFinalDelJuego;
    //Numero de gemas que se han pillado
    float gemasPilladas = 0;
    //Numero de gemas totales a recoger
    float gemasTotales;
    //Variable usada para singelton
    public static UIJuegoScene instance;
    //Variable para activar y desactivar el cronometro
    bool chronometerActive = false;
    //Variable para guardar el tiempo
    float tiempo;
    //Boton para volver al menu inicial
    [SerializeField] Button botonVolverAlMenu;
    //Indice de la escena del menu de inicio
    [SerializeField] int indiceMenu;
    #endregion
    #region Funciones de Unity
    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        //Se consigue este componente
        uIPrep = GetComponent<UIForPreparingJuegoScene>();
        //Se comprueba que este canvas solo se active en la escena de juego,
        //cuando se han detectado los planos necesarios para poder jugar y le des al boton de jugar 
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
    
    private void Update()
    {
        //Comprobamos si se ha activado el cronometro o no
        if (chronometerActive)
        {
            //Si el numero de gemas pilladas es mayor o igual al numero de gemas totales
            if (gemasPilladas >= gemasTotales)
            {
                //Se desactiva el cronometro y se muestran el texto de victoria y el boton para regresar al menu principal
                chronometerActive = false;
                textoFinalDelJuego.text = "Felicidades, ganaste la partida jugador";
                botonVolverAlMenu.gameObject.SetActive(true);
            }
            //Si el tiempo es mayor o igual a 0
            else if (tiempo >= 0)
            {
                //Se resta el tiempo
                tiempo -= Time.deltaTime;
                //Y se actualiza el cronometro en pantalla
                float minutes = Mathf.FloorToInt(tiempo / 60);
                float seconds = Mathf.FloorToInt(tiempo % 60);
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            //Si el tiempo es menor a 0
            else if (tiempo < 0)
            {
                //El tiempo se establece en 0
                tiempo = 0;
                //Se actualiza el cronometro en pantalla
                float minutes = Mathf.FloorToInt(tiempo / 60);
                float seconds = Mathf.FloorToInt(tiempo % 60);
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                //Se desactiva el cronometro
                chronometerActive = false;
                //Se muestra el texto de haber perdido
                textoFinalDelJuego.text = "Mala suerte jugador, perdiste";
                //Se activa el boton para regresar al menu
                botonVolverAlMenu.gameObject.SetActive(true);
            }
        }
    }
    #endregion
    #region Metodos
    //Funcion para actualizar las gemas pilladas
    internal void ActualizarGemasPilladas()
    {
        //Se suma la gema que se ha pillado
        gemasPilladas++;
        //Se actualiza el texto de las gemas
        textoGemas.text = gemasPilladas.ToString() + " / " + gemasTotales.ToString();
    }
    #endregion
    #region Getters y Setters
    //Se setean los parametros para el juego de las gemas
    internal void SetParametrosEnJuegoGemas(float gemasHorizontales, float gemasVerticales, float tiempo)
    {
        //Se guarda el tiempo y se actualiza por pantalla
        this.tiempo = tiempo;
        float minutes = Mathf.FloorToInt(tiempo / 60);
        float seconds = Mathf.FloorToInt(tiempo % 60);
        gemasTotales = gemasHorizontales + gemasVerticales;
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Se actualiza por pantalla el texto de las gemas
        textoGemas.text = gemasPilladas.ToString() + " / " + gemasTotales.ToString();
        //Se activa el cronometro
        chronometerActive = true;
        //Se desactiva el boton de regresar al menu de inicio
        botonVolverAlMenu.gameObject.SetActive(false);
    }
    //Se consigue este canvas
    internal Canvas GetJuegoGemasCanvas()
    {
        return canvasJuego;
    }
    //Se consiguen las gemas totales
    internal float GetGemasTotales()
    {
        return gemasTotales;
    }
    #endregion
}
