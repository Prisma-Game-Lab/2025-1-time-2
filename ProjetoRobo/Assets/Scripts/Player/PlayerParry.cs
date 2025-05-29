using System.Collections;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    private PlayerController pc;
    private InputManager inputManager;

    [Header("Parry Configuration")]
    [SerializeField] private float parryWindow = 0.3f;

    private bool isParryActive = false;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        inputManager = GetComponent<InputManager>();

        // Assign self to PlayerController reference
        pc.playerParry = this;

        // Subscribe to input event
        inputManager.OnParry.AddListener(OnParryPressed);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
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

    public void AttemptParry(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isParryActive)
            {
                Debug.Log("Parried!");
            }
            else
            {
                Debug.Log("Hit");
            }
        }
        else
        {
            Debug.Log("Parry Missed!");
        }

    }
}
