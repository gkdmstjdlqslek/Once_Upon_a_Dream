using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
   public void MenuClick()
   {
        SceneManager.LoadScene("LogIn");
   }

    public void AccountClick()
    {
        SceneManager.LoadScene("Play");
    }

    public void NewClick()
    {
        SceneManager.LoadScene("NewAccount");
    }
}
