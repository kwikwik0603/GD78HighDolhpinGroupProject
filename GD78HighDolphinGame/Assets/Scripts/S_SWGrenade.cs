using System.Collections;
using UnityEngine;

public class S_SWGrenade : MonoBehaviour
{
    public float explosionRadius = 10f;
    public float explosionForce = 2000f;
    public float upwardModifier = 2f;
    public float fuseTime = 2f;
    public bool enableFallDamage = true;

    //public GameObject explosionEffect;  // Assign particle effect in Inspector

    private void Start()
    {
        Invoke("Explode", fuseTime); // Detonate after fuseTime seconds
    }

    private void Update()
    {
        Invoke("Explode", fuseTime);
    }

    private void Explode()
    {
        // Play Explosion Effect
        /*if (explosionEffect) {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }*/
            
        // Find all colliders in explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject == gameObject){
                continue;
            } // Ignore the grenade itself
            
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Debug.Log("Applying force to: " + nearbyObject.name);

            if (rb != null)
            {
                // Calculate explosion direction
                Vector3 explosionDirection = (nearbyObject.transform.position - transform.position).normalized;

                rb.linearVelocity = Vector3.zero; // Reset velocity
                rb.angularVelocity = Vector3.zero; // Reset rotation speed
                rb.AddForce(explosionDirection * explosionForce + Vector3.up * upwardModifier, ForceMode.Impulse);

                // Apply temporary fall damage immunity if it's a player
                /*PlayerController player = nearbyObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.StartCoroutine(player.TemporarilyDisableFallDamage(2f));
                }*/
            }
        }

        // Destroy grenade after explosion
        Destroy(gameObject);
    }

    /*
    private void DisableFallDamage(float duration) {
        enableFallDamage = false;
        StartCoroutine(ReenableFallDamage(duration));
    }

    private IEnumerator ReenableFallDamage(float duration) {
        yield return new WaitForSeconds(duration);
        enableFallDamage = true;
    }
    */
}
