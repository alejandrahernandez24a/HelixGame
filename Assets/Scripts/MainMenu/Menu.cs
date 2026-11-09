using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

     public void EmpezarElJuego(string nombreEscena)
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void SalirdDelJuego()
   {
        Debug.Log("Saliendo del juego...");
      Application.Quit();
   }

  
}
