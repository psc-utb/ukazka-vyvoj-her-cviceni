using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("OutdoorsScene", LoadSceneMode.Single);
    }
}
