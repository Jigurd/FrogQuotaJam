using UnityEngine;

public class CombatActor : MonoBehaviour
{

    [SerializeField]
    private float _attackCooldown = 0.3f;

    private float TimeOfLastAttack = 0;

    [SerializeField]
    public float AttackRange;

    [SerializeField]
    private LayerMask _attackMask = 0;

    [SerializeField]
    private int _damage = 2;

    private Vector3 debugpos2;
    private Vector3 debugpos1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }
    }

    /// <summary>
    /// Launch an attack at the target position
    /// </summary>
    /// <param name="targetPosition">The position the attack is towards</param>
    public void Attack(Vector3 targetPosition)
    {
        Vector2 test = new Vector2(targetPosition.x, targetPosition.y);

        //find out what way we are attacking
        Vector3 dir = ((Vector3)test - transform.position).normalized;

        Vector3 attackPosition = transform.position + (dir*AttackRange);

        debugpos2 = attackPosition;

        if (Time.time >= TimeOfLastAttack + _attackCooldown)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, AttackRange, _attackMask);

            //Debug.Log(enemiesToDamage.Length);

            foreach (Collider2D enemy in enemiesToDamage)
            {
                enemy.gameObject.GetComponent<Damageable>().Damage(_damage);
            }
        }
    }

}
