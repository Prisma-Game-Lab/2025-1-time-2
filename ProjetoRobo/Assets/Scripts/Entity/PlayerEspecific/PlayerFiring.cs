using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFiring : MonoBehaviour
{
    //Reference to controller
    private PlayerController pc;

    [Header("Aim Configuration")]
    [SerializeField] private GameObject aimPrefab;
    [SerializeField] private float aimMaxSpeed;
    [SerializeField] private float aimAccelerationRate;
    [SerializeField] private float aimDesaccelerationRate;
    [SerializeField] private float aimRadius;

    [Header("Mortar Shot Configuration")]
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private int mortarDamage;

    [SerializeField] public int maxAmmo;

    [SerializeField] private float shotTime;
    [SerializeField] private LayerMask targetLayerMask;

    private int ammoCount;
    public GameObject aimObject { get; private set; }
    private Rigidbody2D aimRb;
    private Vector2 aimInput;

    [Header("Meele Attack Configuration")]
    [SerializeField] private GameObject meleeAttackObject;
    [SerializeField] private int meleeAttackDamage;
    [SerializeField] private float meleeAttackDuration;

    [Header("Playtest Mortar Shot Configuration")]
    //Playtest Only
    [SerializeField] private GameObject shotPrefab1;
    [SerializeField] private GameObject shotPrefab2;
    [SerializeField] private GameObject shotPrefab3;
    [SerializeField] private float shot1Delay;
    [SerializeField] private float shot2minDur;
    [SerializeField] private float shot2maxDur;
    [SerializeField] private float shot3speed;

    public CrosshairVisual Crosshair { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent<int> OnAmmoAmountChanged;

    private bool meleeAtacking;

    private void Start()
    {
        pc = GetComponent<PlayerController>();

        aimObject = Instantiate(aimPrefab, transform.position, Quaternion.identity);
        Crosshair = aimObject.GetComponent<CrosshairVisual>();
        aimRb = aimObject.GetComponent<Rigidbody2D>();
        ammoCount = maxAmmo;
    }

    private void FixedUpdate()
    {
        MoveAim();
        RestrictCenter();
        UpdateRotation();
        if (meleeAtacking) MoveMeleeAttack();
    }

    public void OnAimInputChanged(Vector2 newInput) 
    {
        aimInput = newInput;
    }

    private void MoveAim() 
    {
        Vector2 targetSpeed = aimInput * aimMaxSpeed;

        Vector2 speedDif = targetSpeed - aimRb.velocity;

        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = aimAccelerationRate;
        }
        else
        {
            accelRate = aimDesaccelerationRate;
        }

        aimRb.AddForce(speedDif * accelRate);
    }

    private void RestrictCenter() 
    {
        if (Vector2.Distance(transform.position, aimObject.transform.position) > aimRadius) 
        {
            Vector2 directionVector = aimObject.transform.position - transform.position;

            directionVector.Normalize();

            aimObject.transform.position = (Vector2)transform.position + directionVector * aimRadius;
        }
    }

    private void UpdateRotation() 
    {
        if (pc.playerBodyRotation == null) return;

        Vector2 dir = (aimObject.transform.position - transform.position).normalized;
        pc.playerBodyRotation.SetDesiredRotation(dir);
        //float angle = Vector2.SignedAngle(Vector2.right, dir);
        //pc.playerBody.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnShotFired()
    {
        if (ammoCount > 0)
        {
            AudioManager.Instance.PlaySFX("player_shot_sfx");
            //Playtest Only
            switch (GameManager.Instance.mortarShotConfiguration)
            {
                case 0:
                    //Travelling Shot
                    GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
                    shot.GetComponent<MortarController>().Initialization(aimObject.transform.position, mortarDamage, shotTime, targetLayerMask);
                    break;
                case 1:
                    //Instantaneus Shot
                    GameObject instaShot = Instantiate(shotPrefab1, aimObject.transform.position, Quaternion.identity);
                    instaShot.GetComponent<MortarController1>().Initialization(mortarDamage, shotTime, shot1Delay, targetLayerMask);
                    break;
                case 2:
                    //Travelling with varying duration Shot
                    GameObject vdShot = Instantiate(shotPrefab2, transform.position, Quaternion.identity);
                    vdShot.GetComponent<MortarController2>().Initialization(aimObject.transform.position, mortarDamage, shot2minDur, shot2maxDur, aimRadius, targetLayerMask);
                    break;
                case 3:
                    //Travelling with varying duration Shot
                    GameObject speedShot = Instantiate(shotPrefab3, transform.position, Quaternion.identity);
                    speedShot.GetComponent<MortarController3>().Initialization(aimObject.transform.position, shot3speed, mortarDamage, aimRadius, targetLayerMask);
                    break;
            }

            ammoCount -= 1;
            OnAmmoAmountChanged.Invoke(ammoCount);
            //GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            //shot.GetComponent<MortarController>().Initialization(aimObject.transform.position, mortarDamage, shotTime, targetLayerMask);
        }
    }

    public void IncreaseAmmoCount(int ammoReceived)
    {
        ammoCount += ammoReceived;

        if (ammoCount > maxAmmo) 
        {
            ammoCount = maxAmmo;
        }
        if (ammoCount < 0)
        {
            ammoCount = 0;
        }
        OnAmmoAmountChanged.Invoke(ammoCount);
    }
    public void IncreaseDamage(int extraDamage)
    {
        mortarDamage += extraDamage;
    }

    public void MeleeAttack()
    {
         AudioManager.Instance.PlaySFX("player_melee_sfx");
        meleeAtacking = true;
        MoveMeleeAttack();
        meleeAttackObject.GetComponentInChildren<MeeleAttackHitbox>().MeleeDamage = meleeAttackDamage;
        meleeAttackObject.SetActive(true);
        StartCoroutine(DisableMeleeAttack());
    }

    private void MoveMeleeAttack() 
    {
        Vector2 attackDirection = aimObject.transform.position - transform.position;
        meleeAttackObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, attackDirection);
    }

    private IEnumerator DisableMeleeAttack() 
    {
        yield return new WaitForSeconds(meleeAttackDuration);
        meleeAtacking = false;
        meleeAttackObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }
}
