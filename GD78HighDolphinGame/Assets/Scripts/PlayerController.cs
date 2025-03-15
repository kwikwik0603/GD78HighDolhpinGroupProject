using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isFallDamageImmune = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody reference
        rb.isKinematic = false;
    }

    public IEnumerator TemporarilyDisableFallDamage(float duration)
    {
        isFallDamageImmune = true;
        yield return new WaitForSeconds(duration);
        isFallDamageImmune = false;
    }

    // Example fall damage logic (Modify based on your game)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && !isFallDamageImmune)
        {
            Debug.Log("Fall damage applied!");
            // Apply fall damage logic here
        }
    }
}