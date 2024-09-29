using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordsButtonsNav : MonoBehaviour
{
    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
