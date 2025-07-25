using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject buildingSprite;
    [SerializeField] private Collider2D buildingCollider;
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private ParticleSystem[] destructionParticles;

    [Header("Variables")]
    [SerializeField] private float timeBeforeDestruction;
    [SerializeField] private float durability;

    private Vector2 contactPoint;
    private Vector2 normalContact;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.dashing)
            {
                //contactPoint = (collision.GetContact(0).point + collision.GetContact(1).point)/2;
                //normalContact = (collision.GetContact(0).normal + collision.GetContact(1).normal) / 2;
                durability--;
                if (durability <= 0) DestroyBuilding();
            }
        }
    }

    public void DestroyBuilding() 
    {
        StartCoroutine(DestructionTimer());
    }

    private IEnumerator DestructionTimer() 
    {
        buildingCollider.enabled = false;
        //impactParticle.transform.position = contactPoint;
        //impactParticle.transform.rotation = Quaternion.LookRotation(-normalContact);
        //impactParticle.SetActive(true);
        foreach (ParticleSystem particle in destructionParticles) 
        {
            particle.Play();
        }
        buildingSprite.SetActive(false);
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }
}
