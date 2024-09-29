using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsNav : MonoBehaviour
{
    [SerializeField] private GameObject _exitPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void OpenRecords()
    {
        SceneManager.LoadScene("Records");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseExit()
    {
        _exitPanel.SetActive(false);
    }

    public void OpenExit()
    {
        _exitPanel.SetActive(true);
    }
}
