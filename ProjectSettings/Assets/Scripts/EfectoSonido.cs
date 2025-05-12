using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip colectar;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControladorSonidos.Instance.EjecutarSonido(colectar);
            Destroy(gameObject);
        }
    }
}
