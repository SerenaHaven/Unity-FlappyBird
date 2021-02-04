using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GameState
{
    Idle,
    Ready,
    Playing,
    GameOver
}

public class Game : MonoBehaviour
{
    public Action onGameOver;
    public Action onGameReady;
    public Action onScoreChange;

    public GameState gameState = GameState.Idle;

    private Bird _bird;
    private Land _land;
    private BG _bg;

    private Transform _pipeStartAnchor = null;
    private Transform _birdStartAnchor = null;
    private float _interval = 1.0f;
    private float _timer = 0.0f;
    private float _pipeSpeed = 2.0f;

    private GameObject _pipePrefab;
    private readonly List<Pipe> _inactives = new List<Pipe>();
    private readonly HashSet<Pipe> _actives = new HashSet<Pipe>();
    private readonly HashSet<Pipe> _toRemoves = new HashSet<Pipe>();

    public int score { get; set; } = 0;

    void Awake()
    {
        _birdStartAnchor = GameObject.Find("BirdStartAnchor").transform;
        _pipeStartAnchor = GameObject.Find("PipeStartAnchor").transform;

        _bird = FindObjectOfType<Bird>();
        _bird.rig2D.simulated = false;
        _bird.transform.position = Vector2.up * 0.25f;

        _bird.Idle();
        _bird.onThrough = OnThrough;
        _bird.onHit = OnHit;

        _land = FindObjectOfType<Land>();
        _land.stopped = false;

        _bg = FindObjectOfType<BG>();

        _pipePrefab = GameObject.Find("Pipe");
        _pipePrefab.SetActive(false);
    }

    private void OnThrough()
    {
        if (gameState == GameState.Playing)
        {
            Audio.PlayOneShot(AudioEnum.Point);
            score++;
            if (onScoreChange != null) { onScoreChange.Invoke(); }
        }
    }

    private void OnHit()
    {
        if (gameState == GameState.Playing)
        {
            Audio.PlayOneShot(AudioEnum.Hit);
            Audio.PlayOneShot(AudioEnum.Die);
            GameOver();
        }
    }

    public void GameReady()
    {
        DespawnPipeAll();
        _bird.transform.position = _birdStartAnchor.position;
        _bird.Idle();
        _bird.SetSkin();
        _bird.rig2D.simulated = false;
        _land.stopped = true;
        _bg.SetSkin();
        score = 0;
        gameState = GameState.Ready;
        if (onGameReady != null) { onGameReady.Invoke(); }
    }

    public void GameStart()
    {
        DespawnPipeAll();
        _bird.transform.position = _birdStartAnchor.position;
        _bird.Idle();
        _bird.rig2D.simulated = true;
        _land.stopped = false;
        score = 0;
        gameState = GameState.Playing;
    }

    public void GameOver()
    {
        StopPipeAll();
        _bird.Die();
        _land.stopped = true;
        gameState = GameState.GameOver;
        if (onGameOver != null) { onGameOver.Invoke(); }
    }

    void Update()
    {
        if (gameState != GameState.Playing) { return; }

        if (Time.time - _timer > _interval)
        {
            var pipe = SpawnPipe();
            pipe.gameObject.SetActive(true);
            pipe.transform.position = _pipeStartAnchor.transform.position + Vector3.up * Random.Range(-0.5f, 1.5f);
            pipe.rig2D.simulated = true;
            pipe.rig2D.velocity = Vector2.left * _pipeSpeed;
            _timer = Time.time;
        }

        foreach (var item in _actives)
        {
            if (item.transform.position.x < -3.0f) { _toRemoves.Add(item); }
        }

        if (_toRemoves != null && _toRemoves.Count > 0)
        {
            foreach (var item in _toRemoves)
            {
                DespawnPipe(item);
                item.gameObject.SetActive(false);
            }
            _toRemoves.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Audio.PlayOneShot(AudioEnum.Wing);
            _bird.Jump();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Audio.PlayOneShot(AudioEnum.Wing);
            _bird.Jump();
        }
    }

    private Pipe SpawnPipe()
    {
        Pipe result;
        if (_inactives.Count > 0)
        {
            int index = _inactives.Count - 1;
            result = _inactives[index];
            _inactives.RemoveAt(index);
        }
        else
        {
            GameObject go = Instantiate(_pipePrefab);
            result = go.GetComponent<Pipe>();
        }
        _actives.Add(result);
        return result;
    }

    private void DespawnPipe(Pipe target)
    {
        if (_inactives.Contains(target) == false) { _inactives.Add(target); }
        _actives.Remove(target);
    }

    private void DespawnPipeAll()
    {
        foreach (var item in _actives)
        {
            if (_inactives.Contains(item) == false) { _inactives.Add(item); }
            item.gameObject.SetActive(false);
        }
        _actives.Clear();
    }

    private void StopPipeAll()
    {
        foreach (var item in _actives)
        {
            item.rig2D.velocity = Vector2.zero;
            item.rig2D.simulated = false;
        }
    }
}