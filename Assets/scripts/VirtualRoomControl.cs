using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;

public class VirtualRoomControl : MonoBehaviour {

    public static bool isAppear = false;

    public List<ItemControl> listHiddenObjects;

    private void Awake()
    {
        isAppear = true;
        GameObject.Find("GameController").GetComponent<GameControl>().AttachRootRoom(this);

        //var planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        //foreach (var plane in planeManager.trackables)
        //{
        //    plane.gameObject.SetActive(false);
        //}

    }

    public ItemControl GetItemControlByItemType(ItemDefine itemDefine)
    {
        foreach(ItemControl itemControl in listHiddenObjects)
        {
            if(itemControl != null && itemControl.itemType == itemDefine)
            {
                return itemControl;
            }
        }
        return null;
    }
}
