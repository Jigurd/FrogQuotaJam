using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _flyingSpeed = 2.0f;
    [SerializeField] private float _groundHorizontalAccelerationLerpValue = 4.0f;
    [SerializeField] private float _groundHorizontalDecelerationLerpValue = 6.0f;

    private Movement _movement;
    private SpriteRenderer _sprite;
    private Damageable _damageable;
    private CombatActor _combatActor;
    private bool _isFlying = false;

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

        HandleMovement();

        //attack is possible
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    private void HandleMovement()
    {
        // Stop flying if we hit the ground.
        if (_isFlying && _movement.collisions.below)
        {
            _isFlying = false;
        }

        //if the player is jumping, add jump velocity to jump
        if (Input.GetButton("Jump")) // TODO: Make jump and fly separate buttons
        {
            // Start flying
            _isFlying = false;
            _movement.velocity.y = _flyingSpeed;
        }

        // Handle left movement.
        if (Input.GetButton("Left"))
        {
            // Accelerate left.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                -_movement.moveSpeed,
                _groundHorizontalAccelerationLerpValue * Time.deltaTime
            );
            _sprite.flipX = false;
        }
        else if (_movement.velocity.x < 0)
        {
            // Decelerate left.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                0.0f,
                _groundHorizontalDecelerationLerpValue * Time.deltaTime
            );
        }

        // Handle right movement.
        if (Input.GetButton("Right"))
        {
            // Accelerate right.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                _movement.moveSpeed,
                _groundHorizontalAccelerationLerpValue * Time.deltaTime
            );
            _sprite.flipX = false;
        }
        else if (_movement.velocity.x > 0)
        {
            // Decelerate right.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                0.0f,
                _groundHorizontalDecelerationLerpValue * Time.deltaTime
            );
        }
    }

    private void Attack()
    {
        _combatActor.Attack(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}
