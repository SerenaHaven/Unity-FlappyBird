using UnityEngine;

public class BGView : MonoBehaviour
{
    [SerializeField] private Sprite[] _bgs = null;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        }
    }

    public void RandomSkin()
    {
        if (_bgs != null && _bgs.Length > 0)
        {
            var index = Random.Range(0, _bgs.Length);
            spriteRenderer.sprite = _bgs[index];
        }
    }
}