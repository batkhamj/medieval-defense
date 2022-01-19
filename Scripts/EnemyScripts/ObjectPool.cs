using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //NOTE-TO-SELF: Use object pool for enemy wave system
    [SerializeField] GameObject enemy;  //enemy prefab
    [SerializeField, Range(0.5f, 30f)] float timer = 1f;  // timer for spawning enemies
    [SerializeField, Range(0, 50)] int poolSize = 5;

    GameObject[] poolContainer; 
   
    void Awake()
    {
        FillContainer(); //fills the object pool with enemy objects
    }

    void FillContainer()
    {
        poolContainer = new GameObject[poolSize];

        for (int i = 0; i < poolContainer.Length; i++)
        {
            poolContainer[i] = Instantiate(enemy, transform);
            poolContainer[i].SetActive(false);
        }
    }

    void Start() 
    {
        StartCoroutine(InstantiateEnemy());   
    }

    void ActivatePoolContainer()
    {
        for(int i = 0; i < poolContainer.Length; i++)
        {
            if(poolContainer[i].activeSelf == false)
            {
                poolContainer[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator InstantiateEnemy()
    {
        while(true)
        {
            ActivatePoolContainer(); //Activates enemies that are not active inside of object pool
            yield return new WaitForSeconds(timer);    
        }
    }
}
