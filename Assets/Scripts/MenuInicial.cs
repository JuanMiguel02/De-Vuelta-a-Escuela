using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
   public void Jugar(){
        
        TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
     }

     public void Reintentar()
     {
          TransicionEscenasUI.Instance.BloqueSalida(2);
     }
     public void MenuPrincipal()
     {
          TransicionEscenasUI.Instance.BloqueSalida(0);
     }

   public void Salir()
     {
          Debug.Log("Salir del juego");
          Application.Quit();
     }
}
