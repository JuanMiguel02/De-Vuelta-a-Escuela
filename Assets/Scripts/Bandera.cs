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
            if (GameManager.Instance != null)
            {
                float tiempoEscena = controladorJuego.ObtenerTiempoJugado();
                GameManager.Instance.SumarTiempo(tiempoEscena);
            }
        ControladorSonidos.Instance.EjecutarSonido(SonidoCambio, 0.8f);
        GameManager.Instance.GuardarPuntosDeRespaldo();
        TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
}
