using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinaceHandler : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    public int PlayerMoney = 0;
    int lastPlayerMoney;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPlayerMoney != PlayerMoney)
        {
            moneyText.SetText("Blood Points: " + PlayerMoney);
            lastPlayerMoney = PlayerMoney;
        }
    }
}
