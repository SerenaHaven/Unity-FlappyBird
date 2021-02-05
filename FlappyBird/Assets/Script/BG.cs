using UnityEngine;

public class BG : MonoBehaviour
{
    private BGView _view;
    private BGView view
    {
        get
        {
            if (_view == null) { _view = GetComponentInChildren<BGView>(); }
            return _view;
        }
    }

    public void SetSkin()
    {
        view.RandomSkin();
    }

    public void SetTileCount(int tileCount)
    {
        view.SetTileCount(tileCount);
    }
}