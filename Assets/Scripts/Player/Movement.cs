using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Vector3 _startPosition;

    //sprite stuff
    public SpriteRenderer sprite;
    [SerializeField]
    public float moveSpeed;

    public Vector3 velocity;

    //jump related stuff
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    //controls how fast actor falls
    [Range(1f, 1.3f)]
    public float fallSpeedMultiplier = 1.1f;

    float gravity;
    public float jumpVelocity;

    /// <summary>
    /// Collision stuff
    /// </summary>
    BoxCollider2D collision;
    RaycastOrigins raycastOrigins;

    public LayerMask collisionMask;

    const float skinWidth = 0.15f;
    private int rayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    public CollisionInfo collisions;


    void Awake()
    {
        _startPosition = transform.position;
    }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

    }

    void Update()
    {
        //apply gravity to velocity
        //if we're falling, apply fallTimeMultiplier to fall faster. This skews our jump arc right.
        if (velocity.y < 0)
        {
            velocity.y += fallSpeedMultiplier * gravity * Time.deltaTime;
        }
        else
        {

            velocity.y += gravity * Time.deltaTime;

        }


        //move actor. If we hit the floor, set y-direction velocity to 0
        Move(velocity * Time.deltaTime);

        if (velocity.y > 0)
        {
            velocity.y = 0;
        }
    }

    /// <summary>
    /// Move the transform by the given vec3
    /// </summary>
    /// <param name="velocity"></param>
    public void Move(Vector3 velocity)
    {
        collisions.Reset();

        UpdateRaycastOrigins();
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);


    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collision.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaySpacing = Mathf.Clamp(rayCount, 2, int.MaxValue);
        verticalRaySpacing = Mathf.Clamp(rayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (rayCount - 1);
        verticalRaySpacing = bounds.size.x / (rayCount - 1);


    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;

            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collision.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }


    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Got Hit!");
        }
    }
}
