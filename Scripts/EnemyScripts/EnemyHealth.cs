using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;
    [SerializeField] int difficultyScale = 1; //changes difficulty by adding int buffer onto enemy health
    int currentEnemyHealth; 

    Enemy enemy;

    void OnEnable()
    {
        currentEnemyHealth = enemyHealth; 
    }
    void Start() 
    {
        enemy = FindObjectOfType<Enemy>();
    }

    void OnParticleCollision(GameObject other) 
    {
        currentEnemyHealth--;
        if(currentEnemyHealth < 1)
        {
            enemy.Reward(); 
            gameObject.SetActive(false);
            enemyHealth += difficultyScale;
        }
    }
}
