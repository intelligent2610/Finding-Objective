using UnityEngine;
using System.Collections;

public class ButterflyControl : MonoBehaviour
{
    private Transform _Target;
    public float _SpeedMove;
    private Transform _TargetRotation;
    
    public void AttachTarget(Transform target, Transform targetRotate)
    {
        _Target = target;
        _TargetRotation = targetRotate;
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_Target != null)
        {
            _TargetRotation.position = transform.position;
            _TargetRotation.rotation = Quaternion.LookRotation(_Target.position - _TargetRotation.position);
            transform.position = transform.position + transform.forward*Time.deltaTime*_SpeedMove;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _TargetRotation.rotation, Time.deltaTime * 80.6f);
            if (Vector3.Distance(transform.position, _Target.position) < 0.3f)
            {
                _Target = null;
                Destroy(gameObject);
            }
        }
    }
}
