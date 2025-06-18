using System.Collections;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    private PlayerController pc;

    [Header("Parry Configuration")]
    [SerializeField] private float parryWindow = 0.3f;

    [SerializeField] private float parryCooldown = 0.5f;

    [SerializeField] private float parryInvincibilityDuration = 0.5f;
    private bool isInvulnerable = false;
    public bool IsInvulnerable => isInvulnerable;

    [Header("Parry Visual")]
    [SerializeField] private GameObject parryEffectPrefab; // prefab to show
    [SerializeField] private float parryEffectDuration = 0.2f;


    private bool isParryActive = false;
    private bool isOnCooldown = false;

    private bool parrySucceeded = false;
    public bool IsParrying => isParryActive;

    

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    public void OnParryPressed()
    {
        if (!isOnCooldown && !isParryActive)
        {
            StartCoroutine(ParryWindowCoroutine());
        }
        
        
        
    }

    private IEnumerator ParryWindowCoroutine()
    {
        isParryActive = true;
         parrySucceeded = false;
      
        OnParryVisual(false);
        
        yield return new WaitForSeconds(parryWindow);
        isParryActive = false;

        
        if (!parrySucceeded)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(parryCooldown);
         isOnCooldown = false;
     }

    private IEnumerator ParryInvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(parryInvincibilityDuration);
        isInvulnerable = false;
    }
    private void OnParryVisual(bool successful)
{
    if (parryEffectPrefab != null)
    {
        GameObject effectInstance = Instantiate(parryEffectPrefab, transform.position, Quaternion.identity, transform);

        ParryHitbox hitbox = effectInstance.GetComponent<ParryHitbox>();
        if (hitbox != null)
        {
            hitbox.Init(this);
        }

        ParryVisual visual = effectInstance.GetComponent<ParryVisual>();
        if (visual != null)
        {
            if (successful) visual.ShowSuccessVisual();
            else visual.ShowAttemptVisual();
        }

        Destroy(effectInstance, parryEffectDuration);
    }
}


    public void SuccessfulParry()
    {
        ParryVisual visual = GetComponent<ParryVisual>();
        if (visual != null)
        {
            visual.ShowAttemptVisual();
        }
        OnParryVisual(true);
        AudioManager.Instance?.PlaySFX("parry_sfx");
        pc.playerFiring.IncreaseAmmoCount(1);
        StartCoroutine(ParryInvulnerabilityCoroutine());
    }

    public bool AttemptParry(GameObject other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Laser"))
        {
            if (isParryActive)
            {
                 parrySucceeded = true;
                SuccessfulParry();
                return true;
            }
            else
            {
                
                StartCoroutine(CooldownCoroutine());
                
            }
        }
        return false;
    }
}
