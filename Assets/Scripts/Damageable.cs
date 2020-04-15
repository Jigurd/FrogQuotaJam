using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Damageable : MonoBehaviour
{
    public int Health { get; private set; }

    public Action OnDie;

    [SerializeField]
    private int _maxHealth = 20;

    public int MaxHealth { get => _maxHealth;
        private set { _maxHealth = value; } }

    public Action OnDamageTaken;

    [SerializeField]
    private GameObject _actionBubblePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Health = _maxHealth;
    }

    void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

        if (Health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Take some damage
    /// </summary>
    /// <param name="damage">How much damage to take</param>
    public void Damage(int damage)
    {
        Health -= damage;

        //create bubble effect
        Instantiate(_actionBubblePrefab, transform.position, Quaternion.identity);

        OnDamageTaken?.Invoke();
    }

    private void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
        
    }
}
