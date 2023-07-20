    using UnityEngine;
using UnityEngine.Events;
public class PickupHandler : MonoBehaviour
{
    public UnityEvent OnPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            OnPickedUp?.Invoke();
            other.gameObject.SetActive(false);
        }
    }

}
