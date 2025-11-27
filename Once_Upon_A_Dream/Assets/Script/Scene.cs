using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
  
    public void NewClick()
    {
        SceneManager.LoadScene("NewAccount");
    }

    public void Login()
    {
        SceneManager.LoadScene("LogIn");
    }
}
