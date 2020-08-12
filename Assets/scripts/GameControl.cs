using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public List<ItemFinding> _ListItemFindings;
    private Dictionary<ItemDefine, ItemUIControl> _ListUIItem;
    public GameObject _ItemPrefab;
    public GameObject _ButterflyPrefab;
    public Text _TvResult;
    public Transform _RootListItemUI;
    public Transform _PositCreateButterfly;
    public Transform _TempButterflyPosit;
    public GameObject _GameMessagePopup;
    private bool _CanSelectNextItem = true;
    private bool _PopupIsShowing = false;
    private VirtualRoomControl _RootRoom;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        _ListUIItem = new Dictionary<ItemDefine, ItemUIControl>();
        MakeListItem();
    }

    public void AttachRootRoom(VirtualRoomControl root)
    {
        _RootRoom = root;
    }

    private void MakeListItem()
    {
        for(int i = _ListItemFindings.Count-1; i >= 0; i--)
        {
            int k = Random.Range(0, i);
            ItemFinding item = _ListItemFindings[k];
            _ListItemFindings[k] = _ListItemFindings[i];
            _ListItemFindings[i] = item;
        }

        for (int i = 0; i <7; i++)
        {
            ItemFinding item = _ListItemFindings[i];
            GameObject game = Instantiate(_ItemPrefab);
            game.transform.SetParent(_RootListItemUI);
            game.transform.localScale = new Vector3(1, 1, 1);
            game.transform.position = Vector3.zero;
            ItemUIControl itemUIControl = game.GetComponent<ItemUIControl>();
            itemUIControl.InitLayout(item);
            _ListUIItem.Add(item.itemType, itemUIControl);
        }
    }

    public IEnumerator ShowMessageSoFar()
    {
        if (!_PopupIsShowing)
        {
            _PopupIsShowing = true;
            _GameMessagePopup.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            _GameMessagePopup.SetActive(false);
            _PopupIsShowing = false;
        }
    }

    public bool CanCatchItem(ItemDefine itemType, bool isSoFar)
    {
        if (_ListUIItem.ContainsKey(itemType))
        {
            if (isSoFar)
            {
                SoundManager.GetInstance().OnItemSoFar();
                StartCoroutine(ShowMessageSoFar());
                return false;
            }
            if (_CanSelectNextItem)
            {
                _ListUIItem[itemType].AnimCatched();
            }
            return _CanSelectNextItem;
        }
        else
        {
            SoundManager.GetInstance().OnClickWrong();
            return false;
        }
    }

    public void ShowItemInfo(ItemDefine itemType)
    {
        if (_ListUIItem.ContainsKey(itemType))
        {
            _TvResult.text = _ListUIItem[itemType].GetItem().name;
        }
    }

    public void DoCatching()
    {
        SoundManager.GetInstance().OnCatched();
        _CanSelectNextItem = false;
    }

    public void Catched(ItemDefine itemType)
    {
        _TvResult.text = "";
        _CanSelectNextItem = true;
        _ListUIItem[itemType].CatchItem(itemType);
    }

    public void DoSuggestItem()
    {
        if (_CanSelectNextItem)
        {
            
            foreach (ItemDefine key in _ListUIItem.Keys)
            {
                if (!_ListUIItem[key].IsCatched()) {
                    List<ItemControl> listHiddenObjects = _RootRoom.listHiddenObjects;
                    foreach (ItemControl itemControl in listHiddenObjects)
                    {
                        if(itemControl.itemType== key)
                        {
                            GameObject game = Instantiate(_ButterflyPrefab);
                            game.transform.position = _PositCreateButterfly.position;
                            game.transform.rotation = _PositCreateButterfly.rotation;
                            ButterflyControl butterflyControl = game.GetComponent<ButterflyControl>();
                            butterflyControl.AttachTarget(itemControl.transform, _TempButterflyPosit);
                            return;
                        }
                    }
                }
            }
        }
    }
}
