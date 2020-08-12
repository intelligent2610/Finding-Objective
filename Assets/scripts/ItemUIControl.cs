using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUIControl : MonoBehaviour
{
    public Text _TvName;
    public Image _LineMiddle;
    private ItemFinding itemFinding;
    public Animator _AnimScale;
    private bool _IsCatched = false;
    // Use this for initialization
    public void InitLayout(ItemFinding item)
    {
        itemFinding = item;
        _TvName.text = item.name + (item.total > 1 ? 
            "(" + item.total + ")" : "");
    }

    public void AnimCatched()
    {
        _AnimScale.Play("ScaleItem");
    }

    public void CatchItem(ItemDefine itemDefine)
    {
        if (itemFinding.total > 0)
        {
            itemFinding.total--;
            _TvName.text = itemFinding.name + (itemFinding.total > 1 ?
                "(" + itemFinding.total + ")" : "");
        }
        if (itemFinding.total == 0)
        {
            _IsCatched = true;
            _LineMiddle.enabled = true;
        }
    }

    public bool IsCatched()
    {
        return _IsCatched;
    }

    public ItemFinding GetItem()
    {
        return itemFinding;
    }

}
