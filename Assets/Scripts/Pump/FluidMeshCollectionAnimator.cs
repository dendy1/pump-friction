using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidMeshCollectionAnimator : MonoBehaviour
{
    [SerializeField] List<GameObject> meshesData;
    [SerializeField] GameObject _meshHolder;
    [SerializeField] Material _fluidMaterial;
    [SerializeField] Material _transparentMaterial;
    [SerializeField] GameObject _turbine;
    
    private Material _defaultMaterial;
    private Queue<GameObject> _meshes;
    private GameObject[] _meshesList;
    private GameObject _activeMesh;
    private bool _routineIsRunning;
    private GameObject _previous;

    private IEnumerator AnimationPlayer()
    {
        for (int i = 0; i < meshesData.Count; i++)
        {
            _turbine.GetComponent<MeshRenderer>().material = _transparentMaterial;
            _meshHolder.GetComponent<MeshFilter>().mesh = meshesData[i].GetComponent<MeshFilter>().sharedMesh;
            yield return new WaitForSeconds(1f / 24);
        }

        _turbine.GetComponent<MeshRenderer>().material = _defaultMaterial;
        _meshHolder.GetComponent<MeshFilter>().mesh = null;
        _routineIsRunning = false;  
    }

    private void Awake()
    {
        _defaultMaterial = _turbine.GetComponent<MeshRenderer>().material;
        _meshHolder.GetComponent<MeshRenderer>().material = _fluidMaterial;
    }
    
    public void StartWaterAnimation()
    {
        _routineIsRunning = !_routineIsRunning;
        if (_routineIsRunning) StartCoroutine(AnimationPlayer());
        else StopCoroutine(AnimationPlayer());
    }

    public void StopWaterAnimation()
    {
        StopAllCoroutines();
        
        _turbine.GetComponent<MeshRenderer>().material = _defaultMaterial;
        _meshHolder.GetComponent<MeshFilter>().mesh = null;
        _routineIsRunning = false;  
    }
}
