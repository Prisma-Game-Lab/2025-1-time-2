using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : CollectibleController
{
    [SerializeField] public int buffDuration;

    [SerializeField] public int nerfDuration;

    private Collider2D c;

    private SpriteRenderer sr;
    private void Awake()
    {
        c = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        AudioManager.Instance.PlaySFX("powerup_sfx");
        Debug.Log("Collected!");
        cantDespawn = true;
        StartCoroutine(BuffSequence());
        
    }

    public IEnumerator BuffSequence()
    {
        c.enabled = false;
        sr.enabled = false;
        
        ActivateBuff();
        yield return new WaitForSeconds(buffDuration);
        DeactivateBuff();

        yield return StartCoroutine(NerfSequence());
    }
    public IEnumerator NerfSequence()
    {
       
        ActivateNerf();
        yield return new WaitForSeconds(nerfDuration);
        
        DeactivateNerf();
       
    }
    protected virtual void ActivateBuff()
    {

        // Buff Logic
        Debug.Log("BUFF ATIVO");
    }

    protected virtual void DeactivateBuff()
    {
        // Undo Buff
        Debug.Log("BUFF DESATIVADO");
    }
    protected virtual void ActivateNerf()
    {
        // Nerf Logic
         Debug.Log("NERF ATIVO");
    }
    protected virtual void DeactivateNerf()
    {
        // Undo Nerf
        Debug.Log("NERF DESATIVADO");
        Despawn();
    }
}
