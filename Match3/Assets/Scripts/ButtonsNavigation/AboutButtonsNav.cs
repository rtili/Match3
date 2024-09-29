using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutButtonsNav : MonoBehaviour
{
    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
