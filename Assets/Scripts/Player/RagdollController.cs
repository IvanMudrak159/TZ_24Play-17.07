using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        SetRagdoll(false);
    }

    public void SetRagdoll(bool isRagdollEnabled)
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !isRagdollEnabled;
        }

        foreach (var col in colliders)
        {
            col.enabled = isRagdollEnabled;
        }
    }
}
