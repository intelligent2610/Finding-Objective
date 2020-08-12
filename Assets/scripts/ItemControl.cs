using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

    public ItemDefine itemType;
    private Transform _MainCameraTrans;
    private GameControl _GameControl;
    private Transform _CatchedPosition;
    public Vector3 _ScaleCatched = new Vector3(1,1,1);
    public Vector3 _AngleCatched;
    private bool _IsMoveing=false;
    private float _SpeedMoveing = 3.8f;

    private void Start()
    {
        _MainCameraTrans = Camera.main.transform;
        _CatchedPosition = _MainCameraTrans.GetChild(0);
        _GameControl = GameObject.Find("GameController").GetComponent<GameControl>();
    }

    private void OnMouseUpAsButton()
    {
        bool isSoFar = Vector3.Distance(transform.position, _CatchedPosition.position) > 2;
        
        if (_GameControl.CanCatchItem(itemType, isSoFar) )
        {

            _GameControl.DoCatching();
            _IsMoveing = true;
            _CatchedPosition.localEulerAngles = _AngleCatched;
            _CatchedPosition.localScale = _ScaleCatched * 0.2f;
            transform.SetParent(_CatchedPosition);
        }
    }

    private void LateUpdate()
    {
        if (_IsMoveing)
        {
            MoveToCam();
        }
    }

    private void MoveToCam()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time
            .deltaTime * _SpeedMoveing);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time
            .deltaTime * _SpeedMoveing);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,1,1), Time
           .deltaTime * _SpeedMoveing);
        if (Vector3.Distance(transform.position, _CatchedPosition.position) < 0.01f)
        {
            _IsMoveing = false;
            _GameControl.ShowItemInfo(itemType);
            transform.localPosition = Vector3.zero;
            Invoke("PlaySoundAndDestroy", 2);
        }
    }

    private void PlaySoundAndDestroy()
    {
        _GameControl.Catched(itemType);
        Destroy(gameObject);
    }
}
