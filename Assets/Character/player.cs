using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public float speed = 5f;

    public GameObject Bullet;

    public InputSystem_Actions player_controls;
    private InputAction move;
    private InputAction attack;
    private InputAction look;
    private bool isGamepad = false;

    private void Awake()
    {
        player_controls = new InputSystem_Actions();
    }

    // Enable and disable player input
    private void OnEnable()
    {
        move = player_controls.Player.Move;
        move.Enable();
        attack = player_controls.Player.Attack;
        attack.Enable();
        look = player_controls.Player.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        look.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player_combat();
        move_player();
    }

    private void move_player()
    {
        Vector2 player_move = move.ReadValue<Vector2>();

        transform.position += new Vector3(player_move.x, player_move.y, 0) * speed * Time.deltaTime;
    }

    void player_combat()
    {
        if (attack.WasPerformedThisFrame())
        {
            GameObject bullet_gameObject = Instantiate(Bullet);

            bullet_gameObject.transform.position = transform.position;

            if (isGamepad)
            {
                Vector2 player_look = look.ReadValue<Vector2>();
                float look_angle = Mathf.Atan2(player_look.y, player_look.x) * Mathf.Rad2Deg;

                bullet_gameObject.transform.rotation = Quaternion.Euler(0, 0, look_angle);
            }
            else
            {
                Vector3 mouse = new Vector3(look.ReadValue<Vector2>().x, look.ReadValue<Vector2>().y, 0);
                Vector3 mouse_world_pos = Camera.main.ScreenToWorldPoint(mouse);
                mouse_world_pos.z = 0;

                Vector3 direction = mouse_world_pos - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet_gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
