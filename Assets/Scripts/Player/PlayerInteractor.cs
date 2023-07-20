using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    public UnityEvent OnWallHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            OnWallHit?.Invoke();
        }
    }
}
