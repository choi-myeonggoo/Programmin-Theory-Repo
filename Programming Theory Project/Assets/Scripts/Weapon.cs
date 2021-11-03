using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackRate;
    [SerializeField] protected float timer = 0;
    [SerializeField] public float attackRange;

    public abstract void Attck();
}