using TMPro;
using UnityEngine;

public class UiScoreChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    public void ChangeScoreText(int currentScore)
    {
        textScore.text = string.Format("Score: {0}", currentScore.ToString());
    }

}
