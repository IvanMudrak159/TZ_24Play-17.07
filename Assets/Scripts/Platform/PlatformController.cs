using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private List<Transform> _activePlatforms;
    [SerializeField] private Transform _platformFloor;
    [SerializeField] private List<GameObject> _platformPrefabs;
    [SerializeField] private int _initialPlatformNumber = 5;
    private float _platformLength;

    private void Awake()
    {
        _platformLength = _platformFloor.localScale.z;
        _activePlatforms = new List<Transform>();
        for (int i = 0; i < _initialPlatformNumber; i++)
        {
            SpawnActivePlatform(i);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Destroy(_activePlatforms[0].gameObject);
            _activePlatforms.RemoveAt(0);
            SpawnActivePlatform(_initialPlatformNumber++);
        }
    }

    private void SpawnActivePlatform(int platformCount)
    {
        int randomIndex = Random.Range(0, _platformPrefabs.Count);
        GameObject platform = Instantiate(_platformPrefabs[randomIndex], Vector3.zero, Quaternion.identity, transform);
        platform.transform.localPosition = -Vector3.forward * platformCount * _platformLength;
        StaticBatchingUtility.Combine(platform);

        _activePlatforms.Add(platform.transform);
    }
}