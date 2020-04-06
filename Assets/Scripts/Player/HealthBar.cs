using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Damageable parent;

    SpriteRenderer barSpriteRenderer; 

    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Damageable>();
        barSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        gameObject.transform.localScale = new Vector3(1f, 0.15f);

        parent.OnDamageTaken += OnDamaged;
    }

    //When parent takes damage
    void OnDamaged() 
    {
        //calculate HP percentage
        float HealthPercentage = (float)parent.Health / (float)parent.MaxHealth;

        //set bar to that full
        gameObject.transform.localScale = new Vector3(HealthPercentage, 1f);


        //set color based on health percentage
        Color newColor = new Color(1, HealthPercentage, 0);

        barSpriteRenderer.color = newColor;

        //Debug.Log(barSpriteRenderer.color.g);
    }
}
