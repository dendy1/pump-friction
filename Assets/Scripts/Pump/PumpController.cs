using UnityEngine;
public class PumpController : MonoBehaviour
{
    [SerializeField] private string pumpTag = "Player";
    
    private GameObject _pumpObject;
    private Animator _pumpAnimator;
    private ObjectDisabler _pumpObjectDisabler;
    private FluidMeshCollectionAnimator _pumpFluidAnimator;
    
    private static readonly int Speed = Animator.StringToHash("speed");

    private GameObject Pump
    {
        get
        {
            if (_pumpObject == null)
            {
                _pumpObject = GameObject.FindGameObjectWithTag(pumpTag);
            }

            return _pumpObject;
        }
    }

    private Animator PumpAnimator
    {
        get
        {
            if (_pumpAnimator == null)
            {
                _pumpAnimator = Pump.GetComponent<Animator>();
            }

            return _pumpAnimator;
        }
    }
    
    private ObjectDisabler PumpObjectDisabler
    {
        get
        {
            if (_pumpObjectDisabler == null)
            {
                _pumpObjectDisabler = Pump.GetComponent<ObjectDisabler>();
            }

            return _pumpObjectDisabler;
        }
    }
    
    private FluidMeshCollectionAnimator PumpFluidAnimator
    {
        get
        {
            if (_pumpFluidAnimator == null)
            {
                _pumpFluidAnimator = Pump.GetComponentInChildren<FluidMeshCollectionAnimator>();
            }

            return _pumpFluidAnimator;
        }
    }
    
    public void DisassemblyAnimation()
    {
        PumpAnimator.SetFloat(Speed, 1);
        EnableAnimation("MainPump");
    }

    public void AssemblyAnimation()
    {
        PumpAnimator.SetFloat(Speed, -1);
    }
    
    public void EnableOutline()
    {
        EnableAnimation("Outlining");
        PumpObjectDisabler.SetObjectsActivity(false);
    }
    
    public void DisableOutline()
    {
        EnableAnimation("Outlining");
        PumpObjectDisabler.SetObjectsActivity(true);
    }

    public void EnableWaterAnimation()
    {
        PumpFluidAnimator.StartWaterAnimation();
    }
    
    public void DisableWaterAnimation()
    {
        PumpFluidAnimator.StopWaterAnimation();
    }

    public void EnableAnimation(string stateName)
    {
        PumpAnimator.enabled = true;
        PumpAnimator.Play(stateName);
    }

    public void DisableAnimation()
    {
        PumpAnimator.enabled = false;
    }

    public void DisableObjects()
    { 
        PumpObjectDisabler.SetObjectsActivity(true);
    }
}