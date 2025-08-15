using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _zombieCount;
    [SerializeField] private int _zombieTotal;

    private float _spawnInterval = 3f;
    private float _timer;

    void Update()
    {
        if (_zombieCount < _zombieTotal)
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawnInterval)
            {
                SpawnRandomZombie();
                _timer = 0;
            }
        }
    }

    private void SpawnRandomZombie()
    {
        if (_spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform randomSpawnPoint = _spawnPoints[randomIndex];

            Instantiate(_zombiePrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
            _zombieCount++;
        }
    }
}
