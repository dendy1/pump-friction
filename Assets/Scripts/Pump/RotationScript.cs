using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField]
    [Range(.1f, 5f)] float _rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed);
    }
}
