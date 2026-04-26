using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
public class UIForPreparingJuegoScene : MonoBehaviour
{
    ARPlaneManager aRPlaneManager;
    [SerializeField]TMP_Text textForHorizontalPlanesDetected;
    [SerializeField]TMP_Text textForVerticalPlanesDetected;
    int horizontalPlanes;
    int verticalPlanes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }
    public void PlanesDetected(ARTrackablesChangedEventArgs<ARPlane> arPlanes)
    {
        horizontalPlanes = 0;
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            // Solo contamos los que están siendo rastreados y son horizontales
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.HorizontalUp || plane.alignment == PlaneAlignment.HorizontalDown))
            {
                horizontalPlanes++;
            }
            if (plane.trackingState != TrackingState.None &&
               (plane.alignment == PlaneAlignment.Vertical))
            {
                verticalPlanes++;
            }
        }
        textForHorizontalPlanesDetected.text = "Planos horizontales: " + " " + horizontalPlanes;
        textForVerticalPlanesDetected.text = "Planos verticales: " + " " + verticalPlanes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
