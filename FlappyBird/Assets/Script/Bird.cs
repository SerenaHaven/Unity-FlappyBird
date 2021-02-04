using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private BirdView _view;
    private BirdView view
    {
        get
        {
            if (_view == null) { _view = GetComponentInChildren<BirdView>(); }
            return _view;
        }
    }

    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D rig2D
    {
        get
        {
            if (_rigidbody2D == null) { _rigidbody2D = GetComponent<Rigidbody2D>(); }
            return _rigidbody2D;
        }
    }

    private float _force = 10.0f;
    private float _maxSpeed = 4.0f;

    public Action onHit;
    public Action onThrough;

    public void Idle()
    {
        view.Idle();
    }

    public void Die()
    {
        view.Die();
    }

    public void Jump()
    {
        rig2D.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        rig2D.velocity = Vector2.ClampMagnitude(rig2D.velocity, _maxSpeed);
        view.Swing();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        var tag = hitInfo.collider.transform.tag;
        if (tag.Equals("Land") == true)
        {
            if (onHit != null) { onHit.Invoke(); }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var tag = hitInfo.transform.tag;
        if (tag.Equals("Pipe") == true)
        {
            if (onHit != null) { onHit.Invoke(); }
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.tag.Equals("Gap") == true)
        {
            if (onThrough != null) { onThrough.Invoke(); }
        }
    }

    public void SetSkin()
    {
        view.RandomSkin();
    }
}