using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActor : MonoBehaviour
{

    [SerializeField]
    private float _attackCooldown = 0.3f;

    private float TimeOfLastAttack = 0;

    [SerializeField]
    private float _attackRange = 1.0f;

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

    }

    /// <summary>
    /// Launch an attack at the target position
    /// </summary>
    /// <param name="targetPosition">The position the attack is towards</param>
    public void Attack(Vector3 targetPosition)
    {

        Debug.Log(targetPosition);
        Debug.Log(transform.position);


        Vector2 test = new Vector2(targetPosition.x, targetPosition.y);

        //find out what way we are attacking
        Vector3 dir = ((Vector3)test - transform.position).normalized;
        Debug.Log(dir);


        Vector3 attackPosition = transform.position + (dir*_attackRange);

        debugpos2 = attackPosition;


        //Debug.Log(attackPosition);
        

        if (Time.time >= TimeOfLastAttack + _attackCooldown)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, _attackRange, _attackMask);

            //Debug.Log(enemiesToDamage.Length);

            foreach (Collider2D enemy in enemiesToDamage)
            {
                enemy.gameObject.GetComponent<Damageable>().Damage(_damage);
            }
        }
    }

}
