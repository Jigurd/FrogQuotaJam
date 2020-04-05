using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement _movement;
    private SpriteRenderer _sprite;
    private Damageable _damageable;
    private CombatActor _combatActor;
    [SerializeField]
    private Camera _camera = null;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _sprite = GetComponent<SpriteRenderer>();
        _damageable = GetComponent<Damageable>();
        _combatActor = GetComponent<CombatActor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

        _handleMovement();

        //attack is possible
        if (Input.GetButtonDown("Attack"))
        {
            _attack();
        }
    }



    private void _handleMovement()
    {
        //reset horizontal speed
        _movement.velocity.x = 0;


        //if the player is jumping, add jump velocity to jump
        if (Input.GetButton("Jump"))
        {
            _movement.velocity.y += _movement.jumpVelocity;
        }
        //move left
        if (Input.GetButton("Left"))
        {
            _movement.velocity.x -= _movement.moveSpeed;
            _sprite.flipX = false;
        }
        //move right
        if (Input.GetButton("Right"))
        {
            _movement.velocity.x += _movement.moveSpeed;
            _sprite.flipX = true;
        }


    }

    private void _attack()
    {
        _combatActor.Attack(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}
