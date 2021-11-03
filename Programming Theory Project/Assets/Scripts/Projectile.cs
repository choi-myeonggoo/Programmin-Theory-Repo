using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] RangeWeapon currentWeapon;
    [SerializeField] float projectileSpeed;
    [SerializeField] float attackDamage;

    Rigidbody rb;
    Transform focalPoint;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentWeapon = transform.parent.gameObject.GetComponent<RangeWeapon>();
    }
    private void OnEnable()
    {
        focalPoint = transform.root.Find("FocalPoint");
        rb.AddForce(focalPoint.forward * projectileSpeed, ForceMode.Impulse);
        Invoke("DestoryProjectile", 5f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile") return;
        if (currentWeapon.owner == other.name) return;

        DestoryProjectile();
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            Unit unit = other.GetComponent<Unit>();
            unit.health.DecreaseCurrentValue(attackDamage);
        }

    }
    private void DestoryProjectile()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        currentWeapon.ReturnProjectile(this);
    }

}
