using UnityEngine;

public class LandView : MonoBehaviour
{
    private float _speed = 2.0f;

    public bool stopped { get; set; } = false;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null) { _spriteRenderer = GetComponent<SpriteRenderer>(); }
            return _spriteRenderer;
        }
    }

    void Update()
    {
        if (stopped == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
            if (transform.position.x <= -1.44f)
            {
                transform.Translate(Vector2.right * 2.88f);
            }
        }
    }

    public void SetTileCount(int tileCount)
    {
        spriteRenderer.size = new Vector2(tileCount * 2 * 2.88f, 0.96f);
    }
}