using UnityEngine;

public class ColisionInferior : MonoBehaviour
{   
    [SerializeField] public AudioClip deathSound;
     private void OnTriggerEnter2D(Collider2D other)
    {
        Player jugador = other.GetComponent<Player>();
        if (jugador != null)
        {   
            ControladorSonidos.Instance.EjecutarSonido(deathSound, 0.5f);
            jugador.Recolocar();
        }
    }
}
