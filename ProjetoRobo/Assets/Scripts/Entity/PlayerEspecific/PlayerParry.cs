using System.Collections;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    private PlayerController pc;

    [Header("Parry Configuration")]
    [SerializeField] private float parryWindow = 0.3f;

    [SerializeField] public float parryCooldown = 0.5f;

    [SerializeField] public float successfulParryCooldown = 0.5f;

    [SerializeField] private float parryInvincibilityDuration = 0.5f;

    [Header("Parry Visual")]
    [SerializeField] private GameObject parryEffectPrefab; // prefab to show
    [SerializeField] private float parryEffectDuration = 0.2f;

    private bool reflectiveParry = false;
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
        if (!pc.defensiveActionUnlocked || pc.defensiveActionBlocked) return;

        if (!isParryActive)
        {
            StartCoroutine(ParryWindowCoroutine());
        }
    }

    private IEnumerator ParryWindowCoroutine()
    {
        if (isOnCooldown) yield break;

        isParryActive = true;
        parrySucceeded = false;
        isOnCooldown = true;

        OnParryVisual(false);

        yield return new WaitForSeconds(parryWindow);
        isParryActive = false;


        if (!parrySucceeded)
        {
            FindObjectOfType<UIManager>()?.TriggerSecondaryCooldown(false,parryCooldown);
            yield return new WaitForSeconds(parryCooldown);
        }
        else
        {
             FindObjectOfType<UIManager>()?.TriggerSecondaryCooldown(false,successfulParryCooldown);
            yield return new WaitForSeconds(successfulParryCooldown);
        }
        isOnCooldown = false;
    }

    
    public void ReflectableParry()
    {
        reflectiveParry = !reflectiveParry;
        Debug.Log("Reflectable activated");
    }
    public void ChangeParryWindow(float value)
    {
        parryWindow += value;
        parryEffectDuration += value;
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
                else
                {
                    visual.ShowAttemptVisual();
                    AudioManager.Instance.PlaySFX("parry_start_sfx");
                }
            }

            Destroy(effectInstance, parryEffectDuration);
        }
    }

    public void SuccessfulParry()
    {
        ParryVisual visual = GetComponent<ParryVisual>();
        
        OnParryVisual(true);
        AudioManager.Instance?.PlaySFX("parry_sfx");
        pc.playerFiring.IncreaseAmmoCount(1);
        pc.playerHealthController.SetIFrames(parryInvincibilityDuration);
    }

    public bool AttemptParry(GameObject other)
    {
        
        
        if (other.CompareTag("Enemy") || other.CompareTag("Laser"))
        {

            if (isParryActive)
            {

                if (other.CompareTag("Laser") && reflectiveParry)
                {
                    Laser laser = other.GetComponent<Laser>();
                    if (laser != null)
                    {
                        Debug.Log("Attempted Reflection");
                        ReflectLaser(laser);
                    }
                }
                if (!parrySucceeded)
                {
                    parrySucceeded = true;
                    SuccessfulParry();

                }
                return true;

            }
            
        }
        return false;
    }
    private void ReflectLaser(Laser laser)
    {
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
            {
            Vector2 oldDirection = rb.velocity.normalized;
            Vector2 newDirection = -oldDirection;
        
            rb.velocity = newDirection * rb.velocity.magnitude;
            laser.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, newDirection));

            
            laser.gameObject.tag = "ReflectedLaser";
            laser.gameObject.layer = LayerMask.NameToLayer("ReflectedLaser");
            }
    }

    public void OnHit()
    {
        if (IsParrying)
        {
            SuccessfulParry();
            return;
        }
    }
}
