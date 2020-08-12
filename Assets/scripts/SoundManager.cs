using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip _SoundClick;
    public AudioClip _SoundSoFar;
    public AudioClip _SoundWrong;
    public AudioClip _SoundCatched;
    public AudioClip _SoundSuggest;

    private static SoundManager _Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _Instance = this;
    }

    public static SoundManager GetInstance()
    {
        return _Instance;
    }
    
    public void OnClickButton()
    {
        AudioSource.PlayClipAtPoint(_SoundClick, Camera.main.transform.position);
    }

    public void OnItemSoFar()
    {
        AudioSource.PlayClipAtPoint(_SoundSoFar, Camera.main.transform.position);
    }

    public void OnClickWrong()
    {
        AudioSource.PlayClipAtPoint(_SoundWrong, Camera.main.transform.position);
    }

    public void OnCatched()
    {
        AudioSource.PlayClipAtPoint(_SoundCatched, Camera.main.transform.position);
    }

    public void OnClickSuggest()
    {
        AudioSource.PlayClipAtPoint(_SoundSuggest, Camera.main.transform.position);
    }
}
