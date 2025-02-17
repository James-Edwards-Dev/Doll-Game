using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public float speed = 5f;

    public GameObject Bullet;

    public InputSystem_Actions player_controls;
    private InputAction move;
    private InputAction attack;

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
    }

    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
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
            //bullet_gameObject.transform.rotation = "Mouse";    
        }
    }
}
