using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] TMP_Text doorText;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject movePlayerTo;
    public void OpenDoor()
    {
        if (transform.GetChild(1).gameObject.activeSelf)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }

    }
    public void CloseDoor()
    {
        if (!transform.GetChild(1).gameObject.activeSelf)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            playerObject.transform.position = movePlayerTo.transform.position;
        }
        
    }
    public void UpdateDoorText(int UpgradeCost)
    {
        doorText.SetText((UpgradeCost).ToString());
    }
    // Start is called before the first frame update
    
}
