using TMPro;
using UnityEngine;


public class DeathScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textHighScore;
    
    
    public void ChangeScoreText(int currentScore)
    {
        textScore.text = string.Format("Score\n{0}", currentScore.ToString());
    }

    public void ChangeHighScoreText()
    {
        var highScore = SaveManager.Load<SavePlayerHighScore>("playerHighScore").saveData.HighScore;
        textHighScore.text = string.Format("High Score\n{0}", highScore.ToString());
    }
    

}
