using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        
        if (GameManager.Instance != null) {
            GameManager.Instance.ReiniciarPuntos();
        }
        if (HUD.Instance != null) {
            Destroy(HUD.Instance.gameObject);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuInicial(string nombre)
    {
        Time.timeScale = 1f;
        
        if (GameManager.Instance != null) {
            GameManager.Instance.ReiniciarPuntos();
        }
        if (HUD.Instance != null) {
            Destroy(HUD.Instance.gameObject);
        }
        SceneManager.LoadScene(nombre);
    }
}
