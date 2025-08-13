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
    public GameObject aimObject;
    [SerializeField] private float aimMaxSpeed;
    [SerializeField] private float aimAccelerationRate;
    [SerializeField] private float aimDesaccelerationRate;
    [SerializeField] private float aimRadius;

    [Header("Mortar Shot Configuration")]
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private int mortarDamage;
    public int maxAmmo;
    [SerializeField] private float shotTime;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private ParticleSystem onShootParticles;

    private int ammoCount;
    private Rigidbody2D aimRb;
    private Vector2 aimInput;

    [Header("Meele Attack Configuration")]
    [SerializeField] private GameObject meleeAttackObject;
    [SerializeField] private Animator meleeAttackAnimator;
    [SerializeField] private int meleeAttackDamage;
    [SerializeField] private float meleeAttackDuration;
    [SerializeField] public float meleeCooldown;

    public CrosshairVisual Crosshair { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent<int> OnAmmoAmountChanged;

    private bool meleeAtacking;
    private bool meleeOnCooldown;

    

    private void Start()
    {
        pc = GetComponent<PlayerController>();

        Crosshair = aimObject.GetComponent<CrosshairVisual>();
        aimRb = aimObject.GetComponent<Rigidbody2D>();
        ammoCount = maxAmmo;
    }

    private void FixedUpdate()
    {
        MoveAim();
        aimRb.transform.Translate(pc.rb.velocity * Time.deltaTime, transform);
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
        if (pc.offensiveActionBlocked || !pc.offensiveActionUnlocked) return;

        if (ammoCount > 0)
        {
            onShootParticles.Play();
            AudioManager.Instance.PlaySFX("player_shot_sfx");

            GameObject shot = Instantiate(shotPrefab, onShootParticles.transform.position, onShootParticles.transform.rotation);
            shot.GetComponent<MortarController>().Initialization(aimObject.transform.position, mortarDamage, shotTime, targetLayerMask);

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
        if (pc.offensiveActionBlocked || !pc.offensiveActionUnlocked) return;

        if (meleeOnCooldown) return;

        AudioManager.Instance.PlaySFX("player_melee_sfx");
        meleeAtacking = true;
        meleeOnCooldown = true;
        MoveMeleeAttack();
        meleeAttackObject.GetComponentInChildren<MeeleAttackHitbox>().MeleeDamage = meleeAttackDamage;
        meleeAttackObject.SetActive(true);
        meleeAttackAnimator.SetFloat("AttackSpeed", 1/meleeAttackDuration);
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
        yield return new WaitForSeconds(meleeCooldown);
        meleeOnCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aimRadius);
    }
}
