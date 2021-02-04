using System;
using UnityEngine;
using UnityEngine.UI;

public class Flasher : MonoBehaviour
{
    private Image _image;
    private Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            return _image;
        }
    }

    private Action _onFinish;

    private float _speed = 3.0f;

    void Update()
    {
        if (image.color.a <= 0.1f)
        {
            gameObject.SetActive(false);
            if (_onFinish != null)
            {
                _onFinish.Invoke();
                _onFinish = null;
            }
        }
        else
        {
            Color color = image.color;
            color.a -= Time.deltaTime * _speed;
            image.color = color;
        }
    }

    public void Flash(Action onFinish = null)
    {
        gameObject.SetActive(true);
        image.color = Color.white;
        _onFinish = onFinish;
    }
}