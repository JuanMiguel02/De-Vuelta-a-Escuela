using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1f;
       
        if (GameManager.Instance != null )
        {
            GameManager.Instance.RestaurarPuntosDesdeRespaldo(); 
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
