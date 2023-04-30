using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputs : MonoBehaviour
{
    [Header("Scriptables")]
    [SerializeField] Game.ScriptableObjects.EventScriptableObject onMouseButtonDown;

    [Header("Key Codes")]
    [SerializeField] KeyCode interactKeycode = KeyCode.E;
    [SerializeField] KeyCode pauseCamKeyCode = KeyCode.P;
    [SerializeField] KeyCode restartSceneKeyCode = KeyCode.F1;

    [Header("Vectors")]
    Vector2 inputMousePosition;
    Vector2 inputMove;

    void Start()
    {
        inputMousePosition = new();
        inputMove = new();
    }
    void Update()
    {
        MouseUpdate();
        InputRestartScene();
    }

    void MouseUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            onMouseButtonDown?.Invoke();
        }
    }

    void InputRestartScene()
    {
        if (Input.GetKeyDown(restartSceneKeyCode))
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        Scene _currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_currentScene.name);
    }


    public bool InteractKeycodePressed()
    {
        return Input.GetKeyDown(interactKeycode);
    }

    public bool PauseCamPressed()
    {
        return Input.GetKeyDown(pauseCamKeyCode);
    }

    public Vector2 MousePositionInput()
    {
        inputMousePosition.Set(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        return inputMousePosition;
    }

    public Vector2 MoveInput()
    {
        inputMove.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return inputMove;
    }
}
