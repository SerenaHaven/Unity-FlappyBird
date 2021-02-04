using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
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

    private Action _onSwitch;

    private float _speed = 2.0f;

    public void Switch(Action onSwitch, Action onFinish = null, float wait = 0.5f)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        SetAlpha(0);
        StartCoroutine(SwitchProcess(onSwitch, onFinish, wait));
        _onSwitch = onSwitch;
    }

    private IEnumerator SwitchProcess(Action onSwitch, Action onFinish = null, float wait = 0.5f)
    {
        float alpha = image.color.a;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * _speed;
            SetAlpha(alpha);
            yield return null;
        }
        if (onSwitch != null) { onSwitch.Invoke(); }
        yield return new WaitForSeconds(wait);
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime * _speed;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0);
        gameObject.SetActive(false);
        if (onFinish != null) { onFinish.Invoke(); }
    }

    private void SetAlpha(float a)
    {
        var color = image.color;
        color.a = a;
        image.color = color;
    }
}