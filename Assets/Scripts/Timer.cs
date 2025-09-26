using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;
    
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        var minutes = Mathf.FloorToInt(_elapsedTime / 60);
        var seconds = Mathf.FloorToInt(_elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
