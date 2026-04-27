using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GenerarGemas : MonoBehaviour
{
    #region Atributos
    //Referencia al Manager de planos
    ARPlaneManager planeManager;
    //Referencia a la UI del juego
    UIJuegoScene juegoScene;
    //Prefab de las gemas
    [SerializeField] GameObject gema;
    //Referencia a la UI de preparacion de juego
    UIForPreparingJuegoScene uIPrep;
    #endregion
    #region Funciones de Unity
    private void Awake()
    {
        //Se consigen en el padre estos componentes
        uIPrep = GetComponentInParent<UIForPreparingJuegoScene>();
        juegoScene = GetComponentInParent<UIJuegoScene>();
        planeManager = GetComponentInParent<ARPlaneManager>();
    }
    void Start()
    {
        //Se comprueba que este componente solo se active en la escena de juego,
        //cuando se han detectado los planos necesarios para poder jugar y le des al boton de jugar
        if (SceneManager.GetActiveScene().buildIndex != 1 && !uIPrep.GetCanvasPrepararJuego().enabled
            || SceneManager.GetActiveScene().buildIndex == 1 && uIPrep.GetCanvasPrepararJuego().enabled)
        {
            this.enabled = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && !uIPrep.GetCanvasPrepararJuego().enabled)
        {
            this.enabled = true;
        }
    }
    #endregion
    #region Funciones para setear el juego
    internal void SetGemasInScene()
    {
        //Pillamos las gemas que tenemos en total
        float gemasTotales = juegoScene.GetGemasTotales();
        //Creamos dos listas temporales, una para almacenar los planos y otra para almacenar los planos que han sido ocupados
        List<ARPlane> arPlanes = new List<ARPlane>();
        List<ARPlane> planosOcupados = new List<ARPlane>();
        //Añadimos cada plano encontrado en el manager de planos en la lista de planos temporal
        foreach (ARPlane aRP in planeManager.trackables)
        {
            arPlanes.Add(aRP);
        }
        //Repetimos este bucle hasta que se hayan puesto todas las gemas especificadas
        for (int i = 0; i < gemasTotales; i++)
        {
            //Se crea un numero aleatorio para ser el indice de la lista
            int randomIndex = Random.Range(0, arPlanes.Count - 1);
            //Si la lista de planos ocupados contiene el plano que se ha escogido
            if (planosOcupados.Contains(arPlanes[randomIndex]))
            {
                //Se volvera a elegir hasta encontrar un plano que no este ocupado
                while (planosOcupados.Contains(arPlanes[randomIndex]))
                {
                    randomIndex = Random.Range(0, arPlanes.Count - 1);
                }
            }
            //Se instanciara la gema en el centro del plano con la rotacion que tiene es plano
            Instantiate(gema, arPlanes[randomIndex].center, Quaternion.LookRotation(arPlanes[randomIndex].transform.forward, arPlanes[randomIndex].transform.up));
            //Se pone el plano a la lista de planos ocupados
            planosOcupados.Add(arPlanes[randomIndex]);
        }
    }
    #endregion
}
