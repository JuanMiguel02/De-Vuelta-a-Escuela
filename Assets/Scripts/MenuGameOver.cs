using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
       Time.timeScale = 1f;
        int indiceActual = SceneManager.GetActiveScene().buildIndex;

        if (GameManager.Instance != null && indiceActual == 2)
        {
            GameManager.Instance.ReiniciarPuntos();
        }
        if (HUD.Instance != null) {
            Destroy(HUD.Instance.gameObject);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverMenuPrincipal(string nombreEscena)
    {
        Time.timeScale = 1f;
        
        if (GameManager.Instance != null) {
            GameManager.Instance.ReiniciarPuntos();
        }
        if (HUD.Instance != null) {
            Destroy(HUD.Instance.gameObject);
        }
        SceneManager.LoadScene(nombreEscena);
    }
}
