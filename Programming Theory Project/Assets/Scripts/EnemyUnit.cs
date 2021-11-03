using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public sealed class EnemyUnit : Unit
{
    //jump function
    enum State {PATROL, TRACKING, ATTACK, DEAD};
    [SerializeField] State currentState = State.PATROL;

    [SerializeField] Weapon currentWeapon;
    [SerializeField] float sightRange;

    float patrolRange = 40f;
    float minCollisionAvoidanceRange = 3f;
    
    
    Vector3 startPosition;
    Vector3 destination;
    Transform player;
    NavMeshAgent nvAgent;

    private void Awake()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        nvAgent.enabled = false;
    }
    private void OnEnable()
    {
        nvAgent.enabled = true;
    }
    private void OnDisable()
    {
        nvAgent.enabled = false;
    }
    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        Debug.Log(startPosition);
        player = GameObject.Find("Player").transform;
        nvAgent.stoppingDistance = minCollisionAvoidanceRange;
        nvAgent.speed = speed;
        StartCoroutine("GetRandomDestination");

    }

    protected override void Update()
    {
        base.Update();
        float distanceToPlayer = (transform.position - player.position).sqrMagnitude;
        switch (currentState)
        {
            case State.PATROL:
                Patrol();
                if (distanceToPlayer < sightRange) currentState = State.TRACKING;
                break;
            case State.TRACKING:
                Tracking();
                if (distanceToPlayer > sightRange) currentState = State.PATROL;
                else if (distanceToPlayer < currentWeapon.attackRange) currentState = State.ATTACK;
                break;
            case State.ATTACK:
                Attack();
                if (distanceToPlayer > currentWeapon.attackRange) currentState = State.TRACKING;
                break;
            case State.DEAD:
                Die();
                break;
            default:
                break;
        }
    }
    void Patrol()
    {
        nvAgent.SetDestination(destination);
    }
    void Attack()
    {
        transform.LookAt(player);
        currentWeapon.Attck();
    }
    void Tracking()
    {
        nvAgent.SetDestination(player.position);
    }
    IEnumerator GetRandomDestination()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(1.5f,3.0f));
        while (gameObject.activeSelf)
        {
            if(currentState == State.PATROL)
            {
                destination = startPosition + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
            }
            yield return wait;
        }
    }

    protected override void Die()
    {
        SpawnManager.ReturnToPool(gameObject);
    }

}
