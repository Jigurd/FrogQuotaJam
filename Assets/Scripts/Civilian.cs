using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Storyteller.Civilians.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Storyteller.Civilians.Remove(gameObject);
    }
}
