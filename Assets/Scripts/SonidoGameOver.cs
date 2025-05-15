using UnityEngine;

public class SonidoGameOver : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoGameOver;
    private AudioSource musicaCamara;

    void Start()
    {
        // Intenta obtener el AudioSource desde la cámara principal
        if (Camera.main != null)
        {
            musicaCamara = Camera.main.GetComponent<AudioSource>();
        }
    }

    public void EjecutarGameOver()
    {
        // Detiene la música de fondo si existe
        if (musicaCamara != null)
        {
            musicaCamara.Stop();
        }

        // Reproduce el sonido de Game Over (si tienes un ControladorSonidos)
        if (ControladorSonidos.Instance != null && sonidoGameOver != null)
        {
            ControladorSonidos.Instance.EjecutarSonido(sonidoGameOver);
        }
    }
}
