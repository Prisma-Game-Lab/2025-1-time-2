using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject buildingSprite;
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private GameObject buildingParticle;

    [Header("Variables")]
    [SerializeField] private float timeBeforeDestruction;

    private Vector2 contactPoint;
    private Vector2 normalContact;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.dashing)
            {
                contactPoint = (collision.GetContact(0).point + collision.GetContact(1).point)/2;
                normalContact = (collision.GetContact(0).normal + collision.GetContact(1).normal) / 2;
                DestroyBuilding();
            }
        }
    }

    public void DestroyBuilding() 
    {
        StartCoroutine(DestructionTimer());
    }

    private IEnumerator DestructionTimer() 
    {
        impactParticle.transform.position = contactPoint;
        buildingParticle.transform.rotation = Quaternion.LookRotation(normalContact);
        impactParticle.transform.rotation = Quaternion.LookRotation(-normalContact);
        impactParticle.SetActive(true);
        buildingParticle.SetActive(true);
        buildingSprite.SetActive(false);
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }
}
