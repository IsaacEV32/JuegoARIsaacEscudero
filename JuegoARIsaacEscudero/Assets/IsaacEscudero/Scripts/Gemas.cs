using UnityEngine;

public class Gemas : MonoBehaviour
{
    [SerializeField] int layerGemas;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGemas)
        {
            UIJuegoScene.instance.ActualizarGemasPilladas();
            AudioManager.instance.PlayPickSoundEffect();
            collision.gameObject.SetActive(false);
        }
    }
}
