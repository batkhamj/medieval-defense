using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Bank bank; 
    [SerializeField] int towerCost = 30;
    [SerializeField] float delay = 1f;
    void Start()
    {
        StartCoroutine(BuildSequence());
    }
   public bool BuildTower(Tower tower, Vector3 position)
   {
        bank = FindObjectOfType<Bank>();
        if(bank.CurrentBalance >= 50)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.WithdrawMoney(towerCost);    
            return true;
        }
        else
        {
            return false;
        }
   }

   IEnumerator BuildSequence()
   {
       foreach(Transform child in transform){
           child.gameObject.SetActive(false);
           
           foreach(Transform grandchild in child){
               grandchild.gameObject.SetActive(false);
           }
       }

       foreach(Transform child in transform){
           child.gameObject.SetActive(true);
           yield return new WaitForSeconds(delay);

           foreach(Transform grandchild in child){
               grandchild.gameObject.SetActive(true);
           }
       }
   }
}
