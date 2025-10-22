using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    InputAction menuAction;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        menuAction = InputSystem.actions.FindAction("Menu");
        menuAction.performed += MenuAction_performed;
    }

    bool menuDisplayed = false;
    private async void MenuAction_performed(InputAction.CallbackContext obj)
    {
        if (menuDisplayed == false)
        {
            menuAction.performed -= MenuAction_performed;
            await SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            Time.timeScale = 0;
            menuDisplayed = true;
            menuAction.performed += MenuAction_performed;
        }
        else
        {
            menuAction.performed -= MenuAction_performed;
            await SceneManager.UnloadSceneAsync("Menu");
            Time.timeScale = 1;
            menuDisplayed = false;
            menuAction.performed += MenuAction_performed;
        }
    }

    private void OnDestroy()
    {
        menuAction.performed -= MenuAction_performed;
    }
}
