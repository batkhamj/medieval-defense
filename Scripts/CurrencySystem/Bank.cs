using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int initialBalance = 200;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI currencyOutput;
     
    public int CurrentBalance { get {return currentBalance; } }
    
    void Awake()
    {
        currentBalance = initialBalance;
        ShowCurrency();
    }

    public void AddMoney(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        ShowCurrency();
    }
    public void WithdrawMoney(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        
        if(currentBalance < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        ShowCurrency();
    }

    void ShowCurrency()
    {
        currencyOutput.text = " Gold: " + currentBalance;   
    }
}
