using UnityEngine;

public class Pipe : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public Rigidbody2D rig2D
    {
        get
        {
            if (_rigidbody2D == null) { _rigidbody2D = GetComponent<Rigidbody2D>(); }
            return _rigidbody2D;
        }
    }
}