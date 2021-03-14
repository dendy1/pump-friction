using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private GameObject neededObject;

    public void SetObjectsActivity(bool isActive)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(isActive);
        }
        neededObject.SetActive(!isActive);
    }

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameController").gameObject.SetActive(true);
    }
}
