using UnityEngine;

//Note: this is a terrible name for this class; please feel free to change it
public class Targeter : MonoBehaviour
{
    [SerializeField] private Transform targetOrigin;
    [SerializeField] private float targetDistance = 1000f;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private FixedJoint holderPosition;
    private AimTarget _currentTarget;
    private bool touching;
    void Update()
    {
        Vector3 origin = transform.position;
        RaycastHit hit;
        AimTarget _target;

        if (Physics.Raycast(origin, targetOrigin.transform.forward, out hit, targetDistance, hitLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<AimTarget>(out AimTarget target))
            {
                SwapTarget(target);
                touching = true;
                if (touching && Input.GetButtonDown("Fire1"))
                {
                    holderPosition.connectedBody = hit.rigidbody;
                    Debug.Log("Grabbed");
                }



                return;
            }
        }

        _currentTarget?.StopTarget();
        touching = false;
        _currentTarget = null;

    }

    private void SwapTarget(AimTarget target)
    {
        if (target != _currentTarget)
        {
            _currentTarget?.StopTarget();
            _currentTarget = target;
            _currentTarget.Target();
        }
    }
    
    
}
