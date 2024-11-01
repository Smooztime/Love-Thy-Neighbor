using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    bool HasShotgun = false;
    bool HasFullAuto = false;
    bool UpgradeScreenActive;
    int UpgradePrice = 500;
    int UpgradeIncrement = 250;
    [SerializeField] PanelSelection[] childArray;
    [SerializeField] Pistol pistolScript;
    [SerializeField] FinaceHandler accountant;
    [SerializeField] PlayerController playerController;
    [SerializeField] List<GameObject> buttonList;
    [SerializeField] DoorHandler doorHandler;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 0; i < childArray.Length; i++)
        {
            childArray[i].RegenerateList();
            childArray[i].AssignCardForSlot();
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public bool GetHasShotgun()
    {
        return HasShotgun;
    }
    public bool GetHasFullAuto()
    {
        return HasFullAuto;
    }
    public void CloseUpgradeWindow()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    public void OpenUpgradeWindow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (accountant.PlayerMoney < UpgradePrice)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetActive(true);
            }
        }
    }
    public void PurchasedUpgrade()
    {
        for (int i = 0; i < childArray.Length; i++)
        {
            childArray[i].RegenerateList();
            childArray[i].AssignCardForSlot();
        }
        accountant.PlayerMoney -= UpgradePrice;
        UpgradePrice += UpgradeIncrement;
        doorHandler.UpdateDoorText(UpgradePrice);
        doorHandler.CloseDoor();
        playerController.upgradeMenuOpen = false;
        pistolScript.UpdateAmmoText();
        CloseUpgradeWindow();
    }
    // Update is called once per frame
    void Update()
    {
      if(accountant.PlayerMoney > UpgradePrice)
        {
            doorHandler.OpenDoor();
        }
        
    }
    
    public void UpgradeDamage()
    {
        pistolScript.DamageUpgrade += .5f;
        pistolScript.AttackSpeedUpgrade -= .15f;
        pistolScript.KickPerShotUpgrade -= .2f;
        pistolScript.MaxKickUpgrade -= .05f;
        pistolScript.MaxAmmoUpgrade -= 5;
        PurchasedUpgrade();
    }
    public void UpgradeAtkSpeed()
    {
        pistolScript.AttackSpeedUpgrade += 0.2f;
        pistolScript.DamageUpgrade -= .16f;
        pistolScript.MaxInaccuracyUpgrade -= .1f;
        pistolScript.AccuracyRecoveryUpgrade -= .1f;
        PurchasedUpgrade();
    }
    public void UpgradeReloadSpeed()
    {
        pistolScript.ReloadSpeedUpgrade += .3f;
        pistolScript.MaxAmmoUpgrade -= 5;
        PurchasedUpgrade();
    }
    public void UpgradeAmmo()
    {
        pistolScript.MaxAmmoUpgrade += 10;
        pistolScript.ReloadSpeedUpgrade -= 0.25f;
        PurchasedUpgrade();
    }
    public void UpgradeChoke()
    {
        pistolScript.ChokeUpgrade += 0.5f;
        pistolScript.DamageUpgrade -= 0.08f;
        pistolScript.PelletsUpgrade -= 2;
        PurchasedUpgrade();
    }
    public void UpgradePellets()
    {
        pistolScript.DamageUpgrade -= 0.08f;
        pistolScript.PelletsUpgrade += 5;
        pistolScript.ChokeUpgrade -= .15f;
        PurchasedUpgrade();
    }
    public void EnableFullAuto()
    {
        HasFullAuto = true;
        pistolScript.IsFullAuto = true;
        pistolScript.AttackSpeedUpgrade += .7f;
        pistolScript.DamageUpgrade -= 0.15f;
        pistolScript.ReloadSpeedUpgrade -= 0.2f;
        pistolScript.MaxInaccuracyUpgrade -= 0.1f;
        pistolScript.InaccuracyPerShotUpgrade += 0.1f;
        pistolScript.AccuracyRecoveryUpgrade -= 0.1f;
        PurchasedUpgrade();
    }
    public void EnableShotgun()
    {
        HasShotgun = true;
        pistolScript.IsShotgun = true;
        pistolScript.DamageUpgrade -= 0.5f;
        pistolScript.KickPerShotUpgrade -= 0.5f;
        pistolScript.MaxKickUpgrade -= 0.5f;
        pistolScript.MaxAmmoUpgrade -=(int)((pistolScript.MaxAmmoUpgrade + pistolScript.maxAmmo) / 1.5);
        pistolScript.ReloadSpeedUpgrade -= 0.25f;
        PurchasedUpgrade();
    }
    public void TheSlug()
    {
        pistolScript.PelletsUpgrade -= 100;
        pistolScript.DamageUpgrade += 5f;
        pistolScript.ReloadSpeedUpgrade += 1f;
        pistolScript.AttackSpeedUpgrade -= 0.7f;
        pistolScript.KickPerShotUpgrade -= .8f;
        pistolScript.MaxKickUpgrade -= .5f;
        pistolScript.InaccuracyPerShotUpgrade += 1.5f;
        pistolScript.MaxInaccuracyUpgrade += 1.5f;
        pistolScript.ChokeUpgrade += 4f;
        PurchasedUpgrade();
    }
    public void AccuracyRecoveryUpgrade()
    {
        pistolScript.AccuracyRecoveryUpgrade += 0.3f;
        pistolScript.InaccuracyPerShotUpgrade -= 0.1f;
        PurchasedUpgrade();
    }
    public void AccuracyPerShotUpgrade()
    {
        pistolScript.InaccuracyPerShotUpgrade += 0.3f;
        pistolScript.MaxInaccuracyUpgrade += 0.3f;
        PurchasedUpgrade();
    }
    public void KickRecoveryUpgrade()
    {
        pistolScript.KickRecoveryUpgrade += 0.3f;
        pistolScript.KickPerShotUpgrade -= 0.1f;
        PurchasedUpgrade();
    }
    public void KickPerShotUpgrade()
    {
        pistolScript.KickPerShotUpgrade += 0.35f;
        pistolScript.MaxKickUpgrade += 0.35f;
        PurchasedUpgrade();
    }
    public void GunHandlingUpgrade()
    {
        pistolScript.KickPerShotUpgrade += 0.25f;
        pistolScript.KickRecoveryUpgrade += 0.15f;
        pistolScript.MaxKickUpgrade += 0.15f;
        pistolScript.InaccuracyPerShotUpgrade += 0.25f;
        pistolScript.MaxInaccuracyUpgrade += 0.15f;
        pistolScript.AccuracyRecoveryUpgrade += 0.15f;
        pistolScript.DamageUpgrade -= 0.1f;
        pistolScript.AttackSpeedUpgrade -= 0.1f;
        PurchasedUpgrade();
    }
}
