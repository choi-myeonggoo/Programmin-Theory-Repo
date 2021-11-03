using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{

    [SerializeField] Projectile projectile;
    Queue<Projectile> projectilePool = new Queue<Projectile>();
    [HideInInspector] public string owner;
    [SerializeField] float manaConsumption;
    [SerializeField] float projectilePoolSize;
    bool isPlayer;

    private void Awake()
    {
        CreateInitPool();
    }
    private void Start()
    {
        if (transform.root.gameObject.name == "Player") isPlayer = true;
        owner = transform.root.gameObject.name;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
    }
    public override void Attck()
    {
        if (timer <= 0f)
        {
            GetProjectile();
            if (isPlayer)
            {
                PlayerController player = transform.root.gameObject.GetComponent<PlayerController>();
                player.mana.DecreaseCurrentValue(manaConsumption);
            }
            timer = attackRate;
        }
    }
    private void CreateInitPool()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            projectilePool.Enqueue(CreateProjectile());
        }
    }
    private Projectile CreateProjectile()
    {
        Projectile newProjectile = Instantiate(projectile, this.transform);
        newProjectile.gameObject.SetActive(false);
        return newProjectile;
    }
    private Projectile GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            var projectile = projectilePool.Dequeue();
            projectile.gameObject.SetActive(true);
            projectile.transform.SetParent(null);
            return projectile;
        }
        else
        {
            Projectile projectile = CreateProjectile();
            projectile.gameObject.SetActive(true);
            projectile.transform.SetParent(null);
            return projectile;
        }
    }
    public void ReturnProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        
        projectile.transform.localPosition = new Vector3(0, 0f, 0f);
        projectile.transform.localScale = new Vector3(3, 1, 3);
        projectile.transform.localEulerAngles = new Vector3(0, 0, 0);
        projectilePool.Enqueue(projectile);
    }
}
