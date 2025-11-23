using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
   public void MenuClick()
   {
        SceneManager.LoadScene("LogIn");
   }
}
