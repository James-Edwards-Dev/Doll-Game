using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public float speed = 5f;
    public int health = 50;

    // Red 50-41, Green 40-31, Blue, 30-21, Yellow 20-11, Purple, 10-0 
    public enum doll_phases
    {
        red, green, blue, yellow, purple
    }
    public doll_phases doll_phase = doll_phases.red;

    public GameObject Bullet;
    public GameObject Bullet_Spawn;

    public InputSystem_Actions player_controls;
    private InputAction move;
    private InputAction attack;
    private InputAction look;
    private bool isGamepad = false;

    private health_bar player_health_bar;

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
        GameObject Canvas = GameObject.FindGameObjectWithTag("Canvas");

        player_health_bar = Canvas.GetComponentInChildren<health_bar>();
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

            //bullet_gameObject.transform.position = transform.position;
            bullet_gameObject.transform.position = Bullet_Spawn.transform.position;

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

    public void damage(int damage)
    {
        health -= damage;

        if (health >= 0)
        {
            die();
            return ;
        }
        else
        {
            update_color_state();
        }
    }

    void update_color_state()
    {
        // Red 50-41, Green 40-31, Blue, 30-21, Yellow 20-11, Purple, 10-1 
        if (health > 40)
        {
            doll_phase = doll_phases.red;
        }
        else if (health > 30)
        {
            doll_phase = doll_phases.green;
        }
        else if (health > 20)
        {
            doll_phase = doll_phases.blue;
        }
        else if (health > 10) 
        {
            doll_phase = doll_phases.yellow;
        }
        else
        {
            doll_phase = doll_phases.purple;
        }

        player_health_bar.updateHearts(((int)doll_phase));
    }

    void die()
    {
        Debug.Log("You are dead");
    }
}
