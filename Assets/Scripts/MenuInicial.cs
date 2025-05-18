using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
     public void Jugar()
     {

          TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
     
     }
     public void Continuar()
     {
          TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
          GameManager.Instance.ReiniciarPuntos();
     }

     public void Reintentar()
     {
          if (GameManager.Instance != null)
          {
               GameManager.Instance.TiempoAcumulado = 0f;
          }
          TransicionEscenasUI.Instance.BloqueSalida(2);
          GameManager.Instance.ReiniciarPuntos();
     }
     public void MenuPrincipal()
     {
          if (GameManager.Instance != null)
          {
               GameManager.Instance.TiempoAcumulado = 0f;
          }
          TransicionEscenasUI.Instance.BloqueSalida(0);
     }

   public void Salir()
     {
          Debug.Log("Salir del juego");
          Application.Quit();
     }
}
