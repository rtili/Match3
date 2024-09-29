[System.Serializable]
public class ScoreSerial
{
    public string playerName;
    public int score;
    public string date;

    public ScoreSerial(string playerName, int score, string date)
    {
        this.playerName = playerName;
        this.score = score;
        this.date = date;
    }
}
