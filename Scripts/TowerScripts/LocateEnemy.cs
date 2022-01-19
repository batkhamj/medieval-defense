using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateEnemy : MonoBehaviour
{
    [SerializeField] Transform ballistaTop;
    [SerializeField] ParticleSystem boltParticles;
    [SerializeField] float towerRange = 15f;
    
    Transform enemy;

    void Update()
    {
        LocateClosestEnemy(); //find closes enemy for towers to target
        float enemyDistance = Vector3.Distance(transform.position, enemy.position);
        ballistaTop.LookAt(enemy); //tower targets the enemy     
        
        if(enemyDistance < towerRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void LocateClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float farthestEnemy = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position); 
            if(enemyDistance < farthestEnemy)
            {
                closestEnemy = enemy.transform;
                farthestEnemy = enemyDistance;
            }
        }
        enemy = closestEnemy; 
    }

    void Attack(bool check)
    {
        var emission = boltParticles.emission;
        emission.enabled = check;
    }
}
