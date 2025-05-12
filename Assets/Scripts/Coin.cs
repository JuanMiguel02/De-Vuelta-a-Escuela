using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);
            Destroy(gameObject); // Opcional, para eliminar la moneda
        }
    }
}