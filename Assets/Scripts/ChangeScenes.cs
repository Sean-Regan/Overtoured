using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ChangeScenes : MonoBehaviour
{
    public string calm_scene_name;
    public string crowded_scene_name;

    public InputActionAsset input_actions;

    private InputAction calm_scene_action;
    private InputAction crowded_scene_action;

    private void OnEnable()
    {
        input_actions.FindActionMap("SceneManagement").Enable();
    }

    private void OnDisable()
    {
        input_actions.FindActionMap("SceneManagement").Disable();
    }

    private void Awake()
    {
        calm_scene_action = InputSystem.actions.FindAction("CalmScene");
        crowded_scene_action = InputSystem.actions.FindAction("CrowdedScene");
    }

    private void Update()
    {
        

        if (calm_scene_action.IsPressed())
        {
            SceneManager.LoadScene(calm_scene_name);
        }
        else if (crowded_scene_action.IsPressed())
        {
            SceneManager.LoadScene(crowded_scene_name);
        }
    }
}
