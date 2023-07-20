using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    public int poolSize = 10;

    private List<GameObject> _activeCubes;
    private List<GameObject> _inactiveCubes;

    private void Start()
    {
        _activeCubes = new List<GameObject>();
        _inactiveCubes = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity, transform);
            CubeInteractor interactor = cube.GetComponentInChildren<CubeInteractor>();
            if (interactor != null)
            {
                interactor.OnDisappear.AddListener(ReturnCubeToPool);
            }
            cube.SetActive(false);
            _inactiveCubes.Add(cube);
        }
    }

    public GameObject GetCubeFromPool()
    {
        if (_inactiveCubes.Count > 0)
        {
            GameObject cube = _inactiveCubes[0];
            _activeCubes.Add(cube);
            _inactiveCubes.RemoveAt(0);
            cube.SetActive(true);
            return cube;
        }
        GameObject newCube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity, transform);
        _activeCubes.Add(newCube);
        return newCube;
    }

    public void ReturnCubeToPool(GameObject cube)
    {
        cube.SetActive(false);
        cube.transform.parent = transform;
        _activeCubes.Remove(cube);
        _inactiveCubes.Add(cube);
    }
}
