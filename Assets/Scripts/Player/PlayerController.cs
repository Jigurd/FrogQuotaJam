using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _flyingSpeed = 2.0f;
    [SerializeField] private float _flyingFallRate = 0.3f;
    [SerializeField] private float _groundHorizontalAccelerationLerpValue = 4.0f;
    [SerializeField] private float _groundHorizontalDecelerationLerpValue = 6.0f;
    [SerializeField] private float _flyingHorizontalAccelerationLerpValue = 4.0f;
    [SerializeField] private float _flyingHorizontalDecelerationLerpValue = 6.0f;
    [SerializeField] private float _flyingVerticalAccelerationLerpValue = 4.0f;
    [SerializeField] private float _flyingVerticalDecelerationLerpValue = 6.0f;

    private Movement _movement;
    private SpriteRenderer _sprite;
    private Damageable _damageable;
    private CombatActor _combatActor;
    private float _originalGravity;

    public bool IsFlying { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _sprite = GetComponent<SpriteRenderer>();
        _damageable = GetComponent<Damageable>();
        _combatActor = GetComponent<CombatActor>();

        _originalGravity = _movement.gravity;
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
        if (IsFlying && _movement.collisions.below)
        {
            IsFlying = false;
            _movement.gravity = _originalGravity;
        }

        // Handle up movement.
        if (Input.GetButton("Up")) // TODO: Make jump and fly separate buttons
        {
            // Start flying
            if (!IsFlying)
            {
                IsFlying = true;
                _movement.gravity = 0.0f;
            }
            // Accelerate up.
            _movement.velocity.y = Mathf.Lerp(
                _movement.velocity.y,
                _flyingSpeed,
                _flyingVerticalAccelerationLerpValue * Time.deltaTime
            );
        }
        else if (IsFlying)
        {
            // Decelerate up.
            _movement.velocity.y = Mathf.Lerp(
                _movement.velocity.y,
                -_flyingFallRate,
                _flyingVerticalDecelerationLerpValue * Time.deltaTime
            );
        }

        // Handle down movement.
        if (IsFlying && Input.GetButton("Down"))
        {
            // Accelerate down.
            _movement.velocity.y = Mathf.Lerp(
                _movement.velocity.y,
                -_movement.moveSpeed,
                _flyingVerticalAccelerationLerpValue * Time.deltaTime
            );
        }
        else if (IsFlying)
        {
            // Decelerate down.
            _movement.velocity.y = Mathf.Lerp(
                _movement.velocity.y,
                -_flyingFallRate,
                _flyingVerticalDecelerationLerpValue * Time.deltaTime
            );
        }
        //else if (IsFlying && Input.GetButtonUp("Down"))
        //{
        //    _movement.velocity.y = 0.0f;
        //}

        //if (Input.GetButtonUp("Down"))
        //{
        //    _movement.velocity.y = 0.0f;
        //}

        // Handle left movement.
        if (Input.GetButton("Left"))
        {
            // Accelerate left.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                -_movement.moveSpeed,
                (IsFlying
                    ? _flyingHorizontalAccelerationLerpValue
                    : _groundHorizontalAccelerationLerpValue) * Time.deltaTime
            );
            _sprite.flipX = false;
        }
        else if (_movement.velocity.x < 0)
        {
            // Decelerate left.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                0.0f,
                (IsFlying
                    ? _flyingHorizontalDecelerationLerpValue
                    : _groundHorizontalDecelerationLerpValue) * Time.deltaTime
            );
        }

        // Handle right movement.
        if (Input.GetButton("Right"))
        {
            // Accelerate right.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                _movement.moveSpeed,
                (IsFlying
                    ? _flyingHorizontalAccelerationLerpValue
                    : _groundHorizontalAccelerationLerpValue) * Time.deltaTime
            );
            _sprite.flipX = false;
        }
        else if (_movement.velocity.x > 0)
        {
            // Decelerate right.
            _movement.velocity.x = Mathf.Lerp(
                _movement.velocity.x,
                0.0f,
                (IsFlying
                    ? _flyingHorizontalDecelerationLerpValue
                    : _groundHorizontalDecelerationLerpValue) * Time.deltaTime
            );
        }
    }

    private void Attack()
    {
        _combatActor.Attack(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}
