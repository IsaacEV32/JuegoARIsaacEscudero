using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class UIForPreparingJuegoScene : MonoBehaviour
{
    ARPlaneManager aRPlaneManager;
    ConfigurationMenu configurationMenu;
    [SerializeField]TMP_Text textForHorizontalPlanesDetected;
    [SerializeField]TMP_Text textForVerticalPlanesDetected;
    int horizontalPlanes;
    int verticalPlanes;
    float horizontalPlanesMin;
    float verticalPlanesMin;
    private void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        configurationMenu = GetComponentInChildren<ConfigurationMenu>();
        horizontalPlanesMin = configurationMenu.GetPlanosHorizontalesMaximos();
        verticalPlanesMin = configurationMenu.GetPlanosVerticalesMaximos();
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
    }
}
