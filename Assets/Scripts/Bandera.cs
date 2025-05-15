using UnityEngine;
using UnityEngine.SceneManagement;
public class BanderaSc : MonoBehaviour
{
[SerializeField] private ControladorJuego controladorJuego;
private bool yaActivado = false;
    [SerializeField] private AudioClip SonidoCambio;

private void OnTriggerEnter2D(Collider2D other)
{
    if (!yaActivado && other.CompareTag("Player"))
    {
        yaActivado = true;
        controladorJuego.DesactivarTemporizador();
        ControladorSonidos.Instance.EjecutarSonido(SonidoCambio, 0.8f);
        TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
}
