using UnityEngine;

public class FPSSetter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;   
    }
}
