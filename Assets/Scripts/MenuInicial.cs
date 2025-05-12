using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
   public void Jugar(){
        
        TransicionEscenasUI.Instance.BloqueSalida(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Salir(){
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
