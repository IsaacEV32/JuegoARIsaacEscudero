using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GenerarGemas : MonoBehaviour
{
    ARPlaneManager planeManager;
    UIJuegoScene juegoScene;
    [SerializeField] GameObject gema;
    UIForPreparingJuegoScene uIPrep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uIPrep = GetComponentInParent<UIForPreparingJuegoScene>();
        juegoScene = GetComponentInParent<UIJuegoScene>();
        planeManager = GetComponentInParent<ARPlaneManager>();
    }
    void Start()
    {
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
    internal void SetGemasInScene()
    {
        float gemasTotales = juegoScene.GetGemasTotales();
        List<ARPlane> arPlanes = new List<ARPlane>();
        List<ARPlane> planosOcupados = new List<ARPlane>();
        foreach (ARPlane aRP in planeManager.trackables)
        {
            arPlanes.Add(aRP);
        }
        for (int i = 0; i < gemasTotales; i++)
        {
            int randomIndex = Random.Range(0, arPlanes.Count);
            if (planosOcupados.Contains(arPlanes[randomIndex]))
            {
                while (planosOcupados.Contains(arPlanes[randomIndex]))
                {
                    randomIndex = Random.Range(0, arPlanes.Count);
                }
            }
            Instantiate(gema, arPlanes[randomIndex].center, Quaternion.identity);
            planosOcupados.Add(arPlanes[randomIndex]);
        }
    }
}
