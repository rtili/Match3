using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonsNav : MonoBehaviour
{
    [SerializeField] private GameObject _pause;

    public void OpenPause()
    {
        _pause.SetActive(true);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClosePause()
    {
        _pause.SetActive(false);
    }
}
