using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;

    Bank bank;
   
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void Reward()
    {
        if(bank == null){ return; }
        bank.AddMoney(reward);
    }

    public void Penalty()
    {
        if(bank == null){ return; }
        bank.WithdrawMoney(penalty);
    }
}
