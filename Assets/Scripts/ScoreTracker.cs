using UnityEngine;

[CreateAssetMenu(fileName = "ScoreTracker", menuName = "ScriptableObjects/ScoreTracker", order = 1)]
public class ScoreTracker : ScriptableObject
{
    [SerializeField] private int score;

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void ResetScore()
    {
        score = 0;
    }
}