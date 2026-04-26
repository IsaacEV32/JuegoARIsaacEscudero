using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class UIJuegoScene : MonoBehaviour
{
    [SerializeField] Canvas canvasJuego;
    UIForPreparingJuegoScene uIPrep;
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
    internal Canvas GetJuegoGemasCanvas()
    {
        return canvasJuego;
    }
}
