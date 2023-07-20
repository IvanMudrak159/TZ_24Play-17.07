using UnityEngine;
using UnityEngine.Events;

public class CubeInteractor : MonoBehaviour
{
    public UnityEvent<GameObject> OnDisappear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.parent.SetParent(null);
            CameraShakeController.Instance.ShakeCamera(2, 0.3f);
        }
        else if (other.CompareTag("CubeDestroy"))
        {
            OnDisappear?.Invoke(transform.parent.gameObject);
        }
    }
}
