using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _nextWaveButton;

    private void OnEnable()
    {
        _spawner.AllEnemySpawned += OnAllAnemySpawned;
        _nextWaveButton.onClick.AddListener(OnNextWaveButtonClicl);
    }

    private void OnDisable()
    {
        _spawner.AllEnemySpawned += OnAllAnemySpawned;
        _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClicl);
    }

    private void OnAllAnemySpawned()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    public void OnNextWaveButtonClicl()
    {
        _spawner.NextWave();
        _nextWaveButton.gameObject.SetActive(false);
    }
}
