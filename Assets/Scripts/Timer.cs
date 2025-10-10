using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;
    
    private float _enemyLevelUpTimeOriginal;
    private float _enemyLevelUpTime;
    
    [SerializeField] private GameObject enemyLevelScript;


    private void Start()
    {
        _enemyLevelUpTimeOriginal = enemyLevelScript.GetComponent<EnemyLevels>().enemyLevelUpTime;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        var minutes = Mathf.FloorToInt(_elapsedTime / 60);
        var seconds = Mathf.FloorToInt(_elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_elapsedTime >= _enemyLevelUpTime)
        {
            GameManager.Instance.onEnemyLevelUp?.Invoke();
            _enemyLevelUpTime += _enemyLevelUpTimeOriginal;
        }
        
    }

}
