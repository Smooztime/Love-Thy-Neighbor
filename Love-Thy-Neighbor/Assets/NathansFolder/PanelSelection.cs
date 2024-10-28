using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSelection : MonoBehaviour
{
    List<GameObject> listToPickFrom;
    [SerializeField] List<GameObject> listOfStandardUpgrades;
    [SerializeField] List<GameObject> listOfShotgunSpecificUpgrades;
    [SerializeField] GameObject ShotGunCard;
    [SerializeField] GameObject FullAutoCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RegenerateList()
    {
        for (int i = 0; i < listToPickFrom.Count; i++)
        {
            listToPickFrom.Clear();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
