using UnityEngine;

public class Land : MonoBehaviour
{
    private LandView _view;
    private LandView view
    {
        get
        {
            if (_view == null) { _view = GetComponentInChildren<LandView>(); }
            return _view;
        }
    }

    public bool stopped
    {
        get { return view.stopped; }
        set { view.stopped = value; }
    }

    public void SetTileCount(int tileCount)
    {
        view.SetTileCount(tileCount);
    }
}