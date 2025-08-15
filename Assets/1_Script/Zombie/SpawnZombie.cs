using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private float _normalWaveDuration = 60f;
    [SerializeField] private float _fastWaveDuration = 30f;
    [SerializeField] private float _normalSpawnInterval = 3f;
    [SerializeField] private float _fastSpawnInterval = 1f;

    [SerializeField] private Animator _waveAnimator;
    
    private string _isFastWaveAnimBool = "IsFastWave";

    private float _waveTimer;
    private float _spawnTimer;
    private bool _isFastWave = false;

    void Start()
    {
        _waveTimer = 0;
        _spawnTimer = 0;
        if (_waveAnimator != null)
        {
            _waveAnimator.SetBool(_isFastWaveAnimBool, _isFastWave);
        }
    }

    void Update()
    {
        _waveTimer += Time.deltaTime;

        float currentWaveDuration = _isFastWave ? _fastWaveDuration : _normalWaveDuration;

        if (_waveTimer >= currentWaveDuration)
        {
            _isFastWave = !_isFastWave;
            _waveTimer = 0;

            if (_waveAnimator != null)
            {
                _waveAnimator.SetBool(_isFastWaveAnimBool, _isFastWave);
            }
        }

        _spawnTimer += Time.deltaTime;

        float currentSpawnInterval = _isFastWave ? _fastSpawnInterval : _normalSpawnInterval;

        if (_spawnTimer >= currentSpawnInterval)
        {
            SpawnRandomZombie();
            _spawnTimer = 0;
        }
    }

    private void SpawnRandomZombie()
    {
        if (_spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform randomSpawnPoint = _spawnPoints[randomIndex];

            Instantiate(_zombiePrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}
