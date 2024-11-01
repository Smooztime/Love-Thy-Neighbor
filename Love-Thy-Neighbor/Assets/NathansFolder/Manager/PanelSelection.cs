using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSelection : MonoBehaviour
{
    List<GameObject> listToPickFrom = new List<GameObject>();
    [SerializeField] List<GameObject> listOfAllUpgrades;
    [SerializeField] List<GameObject> listOfStandardUpgrades;
    [SerializeField] List<GameObject> listOfShotgunSpecificUpgrades;
    [SerializeField] GameObject ShotGunCard;
    [SerializeField] GameObject FullAutoCard;
    [SerializeField] Upgrade upgradeScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RegenerateList()
    {
        for(int i = 0; i < listOfAllUpgrades.Count; i++)
        {
            listOfAllUpgrades[i].SetActive(true);
        }
        listToPickFrom.Clear();
        for (int i = 0; i < listOfStandardUpgrades.Count; i++)
        {
           listToPickFrom.Add(listOfStandardUpgrades[i]);
        }
        if (upgradeScript.GetHasFullAuto())
        {

        }
        else
        {
           listToPickFrom.Add(FullAutoCard);
        }
        if (upgradeScript.GetHasShotgun())
        {
            for (int i = 0; i < listOfShotgunSpecificUpgrades.Count; i++)
            {
                listToPickFrom.Add(listOfShotgunSpecificUpgrades[i]);
            }
        }
        else
        {
            listToPickFrom.Add(ShotGunCard);
        }
        for (int i = 0; i < listOfAllUpgrades.Count; i++)
        {
            listOfAllUpgrades[i].gameObject.SetActive(false);
        }
    }
    public void AssignCardForSlot()
    {
        for(int i = 0; i < listOfAllUpgrades.Count; i++)
        {
            listOfAllUpgrades[i].gameObject.SetActive(false);
        }
        int randIndex = Random.Range(0, listToPickFrom.Count);
        listToPickFrom[randIndex].gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
