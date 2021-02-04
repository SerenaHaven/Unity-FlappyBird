using UnityEngine;

public class BirdView : MonoBehaviour
{
    private Animator _animator;
    private Animator animator
    {
        get
        {
            if (_animator == null) { _animator = GetComponentInChildren<Animator>(); }
            return _animator;
        }
    }

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null) { _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); }
            return _spriteRenderer;
        }
    }

    private Sprite[] _sprites = null;
    [SerializeField] private Sprite[] _skin1 = null;
    [SerializeField] private Sprite[] _skin2 = null;
    [SerializeField] private Sprite[] _skin3 = null;

    private bool _swinging = false;

    void Awake()
    {
        _sprites = _skin1;
    }

    void Update()
    {
        if (_swinging == true && _sprites != null && _sprites.Length > 0)
        {
            var index = Time.frameCount / 8 % _sprites.Length;
            spriteRenderer.sprite = _sprites[index];
        }
    }

    public void Idle()
    {
        _swinging = true;
        animator.Play("Bird_Float", 0, 0);
    }

    public void Swing()
    {
        _swinging = true;
        animator.Play("Bird_Dive", 0, 0);
    }

    public void Die()
    {
        _swinging = false;
        animator.Play("Bird_Dive", 0, 1.0f);
    }

    public void RandomSkin()
    {
        var index = Random.Range(0, 3);
        switch (index)
        {
            case 1:
                _sprites = _skin1;
                break;
            case 2:
                _sprites = _skin2;
                break;
            case 3:
                _sprites = _skin3;
                break;
            default:
                _sprites = _skin1;
                break;
        }

        spriteRenderer.sprite = _sprites[0];
    }
}