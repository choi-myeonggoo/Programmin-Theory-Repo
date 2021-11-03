using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : MonoBehaviour
{

    [SerializeField] protected float speed;
    protected Rigidbody rb;
    public Energy health = new Energy();

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        health.RecoverAll();
    }
    protected virtual void Update()
    {
        if (health.CurrentValue <= 0) Die();
        if (health.CurrentValue < health.MaxValue) RegenerateHealth();
    }

    void RegenerateHealth()
    {
        health.IncreaseCurrentValue(health.Regeneration * Time.deltaTime);
    }
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "die");
        gameObject.SetActive(false);
    }
}

