using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActor : MonoBehaviour
{

    [SerializeField]
    private float _attackCooldown = 0.3f;

    private float TimeOfLastAttack = 0;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private LayerMask _attackMask;

    [SerializeField]
    private int _damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Launch an attack at the target position
    /// </summary>
    /// <param name="targetPosition">The position the attack is towards</param>
    public void Attack(Vector3 targetPosition)
    {
        //find out what way we are attacking
        Vector3 dir = (transform.position - targetPosition).normalized;
        Vector3 attackPosition = dir * _attackRange;

        Debug.DrawRay(transform.position, dir, Color.red, 4);
        //Gizmos.DrawWireSphere(attackPosition, _attackRange);

        if (Time.time >= TimeOfLastAttack + _attackCooldown)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, _attackRange, _attackMask);

            foreach (Collider2D enemy in enemiesToDamage)
            {
                enemy.gameObject.GetComponent<Damageable>().Damage(_damage);
            }
        }
    }

}
