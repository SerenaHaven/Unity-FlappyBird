using UnityEngine;

public class LandView : MonoBehaviour
{
    private float _speed = 2.0f;

    public bool stopped { get; set; } = false;

    void Update()
    {
        if (stopped == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
            if (transform.position.x <= -2.88f)
            {
                transform.Translate(Vector2.right * 2.88f);
            }
        }
    }
}