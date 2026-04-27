using UnityEngine;

public class Gemas : MonoBehaviour
{
    //Guardamos la layer de las gemas
    [SerializeField] int layerGemas;
    //Comprobamos que el jugador haya colisionado con una gema
    private void OnCollisionEnter(Collision collision)
    {
        //Si la layer del objeto colisionado es igual al de las gemas
        if (collision.gameObject.layer == layerGemas)
        {
            //Se actualiza el numero de gemas que se han pillado
            UIJuegoScene.instance.ActualizarGemasPilladas();
            //Se reproduce el sonido de agarrar gemas
            AudioManager.instance.PlayPickSoundEffect();
            //Se desactiva el objeto
            collision.gameObject.SetActive(false);
        }
    }
}
