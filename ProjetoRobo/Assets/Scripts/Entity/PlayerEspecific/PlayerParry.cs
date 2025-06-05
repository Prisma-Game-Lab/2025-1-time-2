using System.Collections;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    private PlayerController pc;

    private PlayerFiring pf;
    private InputManager inputManager;

    [Header("Parry Configuration")]
    [SerializeField] private float parryWindow = 0.3f;

    [Header("Parry Visual")]
    [SerializeField] private GameObject parryEffectPrefab; // prefab to show
    [SerializeField] private float parryEffectDuration = 0.2f;

    private bool isParryActive = false;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        inputManager = GetComponent<InputManager>();
        pf = GetComponent<PlayerFiring>();

        pc.playerParry = this;
        inputManager.OnParry.AddListener(OnParryPressed);
    }

    private void OnDestroy()
    {
        inputManager.OnParry.RemoveListener(OnParryPressed);
    }

    private void OnParryPressed()
    {
        
        StartCoroutine(ParryWindowCoroutine());
        
    }

    private IEnumerator ParryWindowCoroutine()
    {
        isParryActive = true;
        yield return new WaitForSeconds(parryWindow);
        isParryActive = false;
    }

    private void OnParryVisual()
    {
        if (parryEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(parryEffectPrefab, transform.position, Quaternion.identity, transform);
            Destroy(effectInstance, parryEffectDuration);
        }
    }

    public bool AttemptParry(GameObject other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Laser"))
        {
            if (isParryActive)
            {
                OnParryVisual();
                if (pf.ammoCount < pf.maxAmmo)
                {
                    pf.ammoCount += 1; // Trigger the visual effect
                }
                Debug.Log("Parried!");
                return true;
            }
            else
            {
                Debug.Log("Hit");
                
            }
        }
        return false;
    }
}
