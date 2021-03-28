using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteAlways]
public class ShaderPropertyHelper : MonoBehaviour
{
    [SerializeField] private Material referenceMaterial;
    // Update is called once per frame
    void Update()=>
        referenceMaterial.SetVector("_PivotVectorForTransparency", transform.position);
    
}
