using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    public InputSystem_Actions player_controls;
    private InputAction move;

    private void Awake()
    {
        player_controls = new InputSystem_Actions();
    }

    // Enable and disable player input
    private void OnEnable()
    {
        move = player_controls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(move.ReadValue<Vector2>());
    }
}
