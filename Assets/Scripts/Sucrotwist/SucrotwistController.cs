using UnityEngine;

public class SucrotwistController : MonoBehaviour
{
    [SerializeField] private string sucrotwistTag = "Player";
    
    private GameObject _sucrotwistObject;
    private Animator _sucrotwistAnimator;

    private GameObject Sucrotwist
    {
        get
        {
            if (_sucrotwistObject == null)
            {
                _sucrotwistObject = GameObject.FindGameObjectWithTag(sucrotwistTag);
            }

            return _sucrotwistObject;
        }
    }

    private Animator SucrotwistAnimator
    {
        get
        {
            if (_sucrotwistAnimator == null)
            {
                _sucrotwistAnimator = Sucrotwist.GetComponent<Animator>();
            }

            return _sucrotwistAnimator;
        }
    }

    public void PlayAnimation()
    {
        SucrotwistAnimator.Play("ComplexAnimation");
    }
}
