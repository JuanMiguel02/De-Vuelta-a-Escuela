using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuInicial(string nombre)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }
}
