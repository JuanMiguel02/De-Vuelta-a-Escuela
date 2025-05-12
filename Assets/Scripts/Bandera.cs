using UnityEngine;
using UnityEngine.SceneManagement;
public class BanderaSc : MonoBehaviour
{
private bool yaActivado = false;

private void OnTriggerEnter2D(Collider2D other)
{
    if (!yaActivado && other.CompareTag("Player"))
    {
        yaActivado = true;
        TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
}
