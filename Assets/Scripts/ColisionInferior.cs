using UnityEngine;

public class ColisionInferior : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
    {
        Player jugador = other.GetComponent<Player>();
        if (jugador != null)
        {
            jugador.Recolocar();
        }
    }
}
