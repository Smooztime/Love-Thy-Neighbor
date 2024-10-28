using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pistol : MonoBehaviour
{

    float accuracyRecoverySpeed => pistolData.AccuracyRecoverySpeed;
    float kickRecoverySpeed => pistolData.KickRecoverySpeed;
    float pistolDamage => pistolData.Damage;
    float attackSpeed => pistolData.AttackSpeed;
    float maxInaccuracy => pistolData.MaxInaccuracy;
    float maxKick => pistolData.MaxKick;
    float reloadSpeed => pistolData.ReloadSpeed;
    bool fullAuto => pistolData.FullAutoMode;
    bool shotGunMode => pistolData.ShotGunMode;
    int maxAmmo => pistolData.MaxAmmo;

    public float AccuracyRecoveryUpgrade { get; set; } = 1;
    public float KickRecoveryUpgrade { get; set; } = 1;
    public float DamageUpgrade { get; set; } = 1;
    public float AttackSpeedUpgrade { get; set; } = 1;
    public float MaxInaccuracyUpgrade { get; set; } = 1;
    public float MaxKickUpgrade { get; set; } = 1;
    public float ReloadSpeedUpgrade { get; set; } = 1;
    public int MaxAmmoUpgrade { get; set; } = 0;
    public float ChokeUpgrade { get; set; } = 1;
    public int PelletsUpgrade { get; set; } = 0;
    public float KickPerShotUpgrade { get; set; } = 1;
    public float InaccuracyPerShotUpgrade { get; set; } = 1;

    [field: SerializeField] public float InAccuracy { get; private set; } = 0;
    [field: SerializeField] public float Kick { get; private set; } = 0;

    public int AmmoInGun;
    public bool mouseIsDown = false;
    bool canShoot = true;
    float kickLerp = 1;

    [SerializeField] GameObject bulletMark;
    [SerializeField] PistolData pistolData;
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        AmmoInGun = maxAmmo + MaxAmmoUpgrade;
       // pistolDamage = pistolData.Damage;
       // attackSpeed = pistolData.AttackSpeed;
       //// accuracyRecoverySpeed = pistolData.accuracyRecoverySpeed;
       // maxInaccuracy = pistolData.MaxInaccuracy;
       // kickRecoverySpeed = pistolData.KickRecoverySpeed;
       // maxKick = pistolData.MaxKick;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(InAccuracy > 0)
        {
            InAccuracy -= Time.deltaTime * accuracyRecoverySpeed * AccuracyRecoveryUpgrade;
        }
        if(InAccuracy < 0)
        {
            InAccuracy = 0;
        }
        if(Kick > 0)
        {
            kickLerp -= Time.deltaTime * kickRecoverySpeed * KickRecoveryUpgrade;
            Kick = Mathf.Lerp(0, Kick, kickLerp);
        }
        else
        {
            Kick = 0;
            kickLerp = 1;
        }
    }
    private void Update()
    {
        FullAutoFire();
        AutoShotGun();
    }
    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(attackSpeed / AttackSpeedUpgrade);
        canShoot = true;
    }
   public IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(reloadSpeed / ReloadSpeedUpgrade);
        AmmoInGun = maxAmmo + MaxAmmoUpgrade;
    }
    public void Reload()
    {
        StartCoroutine(ReloadGun());
    }

    void AutoShotGun()
    {
        if(shotGunMode && fullAuto && canShoot && mouseIsDown && AmmoInGun > 0)
        {
            kickLerp = 1;
            AmmoInGun--;
            Kick += pistolData.PerShotKick / KickPerShotUpgrade;
            Kick = Mathf.Clamp(Kick, 0, maxKick / MaxKickUpgrade);
            for (int i = 0; i < pistolData.ShotgunPellets + PelletsUpgrade; i++)
            {
                RaycastHit hit;
                float xSpread = Random.Range(-pistolData.ShotgunSpread / ChokeUpgrade, pistolData.ShotgunSpread/ChokeUpgrade);
                float ySpread = Random.Range(-pistolData.ShotgunSpread / ChokeUpgrade, pistolData.ShotgunSpread/ChokeUpgrade);
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xSpread, 0.5f + ySpread, 0));
                Debug.DrawRay(ray.origin, ray.direction * 10);
                if (Physics.Raycast(ray, out hit))
                {
                    Instantiate(bulletMark, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.TryGetComponent(out HeadHit head))
                    {
                        head.HeadWasHit(pistolDamage * DamageUpgrade);
                    }
                    if (hit.collider.gameObject.TryGetComponent(out BodyHit body))
                    {
                        body.BodyWasHit(pistolDamage * DamageUpgrade);
                    }
            }

            playerController.GunKickUp(Kick);
            StartCoroutine(ShotCoolDown());
            canShoot = false;
        }
    }
    }
    public void ShotGunFire()
    {
        if (shotGunMode && canShoot && !fullAuto && AmmoInGun > 0)
        {
            kickLerp = 1;
            AmmoInGun--;
            Kick += pistolData.PerShotKick / KickPerShotUpgrade;
            Kick = Mathf.Clamp(Kick, 0, maxKick / MaxKickUpgrade);
            for (int i = 0; i < pistolData.ShotgunPellets + PelletsUpgrade; i++)
            {
                RaycastHit hit;
                float xSpread = Random.Range(-pistolData.ShotgunSpread / ChokeUpgrade, pistolData.ShotgunSpread/ChokeUpgrade);
                float ySpread = Random.Range(-pistolData.ShotgunSpread / ChokeUpgrade, pistolData.ShotgunSpread/ ChokeUpgrade);
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xSpread, 0.5f + ySpread, 0));
                Debug.DrawRay(ray.origin, ray.direction * 10);
                if (Physics.Raycast(ray, out hit))
                {
                    Instantiate(bulletMark, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.TryGetComponent(out HeadHit head))
                    {
                        head.HeadWasHit(pistolDamage * DamageUpgrade);
                    }
                    if (hit.collider.gameObject.TryGetComponent(out BodyHit body))
                    {
                        body.BodyWasHit(pistolDamage * DamageUpgrade);
                    }

                }
            }

            playerController.GunKickUp(Kick);
            StartCoroutine(ShotCoolDown());
            canShoot = false;
        }
    }
    public void FullAutoFire()
    {
        if(canShoot && mouseIsDown && fullAuto && !shotGunMode && AmmoInGun > 0)
        {
            kickLerp = 1;
            AmmoInGun--;
            float xBloom = Random.Range(-InAccuracy , InAccuracy);
            InAccuracy += pistolData.PerShotRecoil / InaccuracyPerShotUpgrade;
            Kick += pistolData.PerShotKick / KickPerShotUpgrade;
            Kick = Mathf.Clamp(Kick, 0, maxKick / MaxKickUpgrade);
            InAccuracy = Mathf.Clamp(InAccuracy, 0, maxInaccuracy / MaxInaccuracyUpgrade);
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xBloom, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10);

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletMark, hit.point, Quaternion.identity);
                if (hit.collider.gameObject.TryGetComponent(out HeadHit head))
                {
                    head.HeadWasHit(pistolDamage * DamageUpgrade);
                }
                if (hit.collider.gameObject.TryGetComponent(out BodyHit body))
                {
                    body.BodyWasHit(pistolDamage * DamageUpgrade);
                }

            }
            playerController.GunKickUp(Kick);
            StartCoroutine(ShotCoolDown());
            canShoot = false;
        
    }
    }
    public void Shoot()
    {
        if(canShoot && !fullAuto && !shotGunMode && AmmoInGun > 0)
        {
            kickLerp = 1;
            AmmoInGun--;
            float xBloom = Random.Range(-InAccuracy, InAccuracy);
            InAccuracy += pistolData.PerShotRecoil / InaccuracyPerShotUpgrade;
            Kick += pistolData.PerShotKick / KickPerShotUpgrade;
            Kick = Mathf.Clamp(Kick, 0, maxKick / MaxKickUpgrade);
            InAccuracy = Mathf.Clamp(InAccuracy, 0, maxInaccuracy / MaxInaccuracyUpgrade);
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xBloom, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10);

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletMark, hit.point, Quaternion.identity);
                if (hit.collider.gameObject.TryGetComponent(out HeadHit head))
                {
                    head.HeadWasHit(pistolDamage * DamageUpgrade);
                }
                if (hit.collider.gameObject.TryGetComponent(out BodyHit body))
                {
                    body.BodyWasHit(pistolDamage * DamageUpgrade);
                }

            }
            playerController.GunKickUp(Kick);
            StartCoroutine(ShotCoolDown());
            canShoot = false;
        }
        
        
    }
}
