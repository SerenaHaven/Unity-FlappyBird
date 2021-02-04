using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    private GameObject _new;
    private Image _imageMedal;
    private Image _imageBlink;
    private Text _textScore;
    private Text _textBest;
    private int _scoreStep = 10;

    public Action onAnimationFinish;
    public int score { get; set; } = 0;
    [SerializeField] private Sprite[] _medals = null;
    [SerializeField] private Sprite[] _blinks = null;

    private Text textScore
    {
        get
        {
            if (_textScore == null) { _textScore = transform.Find("BG/TextScore").GetComponent<Text>(); }
            return _textScore;
        }
    }

    private Text textBest
    {
        get
        {
            if (_textBest == null) { _textBest = transform.Find("BG/TextBest").GetComponent<Text>(); }
            return _textBest;
        }
    }

    private int best
    {
        get { return PlayerPrefs.GetInt("Best", 0); }
        set { PlayerPrefs.SetInt("Best", value); }
    }

    void Awake()
    {
        _new = transform.Find("BG/New").gameObject;
        _new.SetActive(false);

        _imageMedal = transform.Find("BG/ImageMedal").GetComponent<Image>();
        _imageMedal.gameObject.SetActive(false);

        _imageBlink = transform.Find("BG/ImageMedal/ImageBlink").GetComponent<Image>();

        _textScore = transform.Find("BG/TextScore").GetComponent<Text>();
        _textBest = transform.Find("BG/TextBest").GetComponent<Text>();
    }

    void OnEnable()
    {
        textBest.text = best.ToString();
        _new.SetActive(false);
        _imageMedal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_imageMedal.gameObject.activeSelf == true && _blinks != null && _blinks.Length > 0)
        {
            var index = Time.frameCount / 14 % _blinks.Length;
            _imageBlink.sprite = _blinks[index];
        }
    }

    private IEnumerator SetScoreProcess()
    {
        var speed = score * 2.0f;
        var current = 0.0f;
        while (current < score)
        {
            current += speed * Time.deltaTime;
            textScore.text = ((int)current).ToString(); ;
            yield return null;
        }
        textScore.text = score.ToString();

        yield return new WaitForSeconds(0.5f);

        if (score >= best)
        {
            current = best;
            while (current < score)
            {
                current += speed * Time.deltaTime;
                textBest.text = ((int)current).ToString(); ;
                yield return null;
            }
            textBest.text = score.ToString();

            best = score;
            _new.SetActive(true);
        }
        else { _new.SetActive(false); }

        yield return new WaitForSeconds(0.5f);

        var index = (int)(score / _scoreStep) - 1;
        index = Mathf.Clamp(index, index, _medals.Length - 1);
        if (index < 0)
        {
            _imageMedal.gameObject.SetActive(false);
        }
        else
        {
            _imageMedal.gameObject.SetActive(true);
            _imageMedal.sprite = _medals[index];
        }

        yield return new WaitForSeconds(0.5f);

        if (onAnimationFinish != null) { onAnimationFinish.Invoke(); }
    }

    private void OnBalance()
    {
        StopAllCoroutines();
        textScore.text = "0";
        StartCoroutine(SetScoreProcess());
    }
}