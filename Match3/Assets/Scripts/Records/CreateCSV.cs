using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateCSV : MonoBehaviour
{
    private string _filePath;

    private void Start()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "Records.csv");
        if (!File.Exists(_filePath))
        {
            CreateAndFillCSV();
        }
    }

    private void CreateAndFillCSV()
    {
        List<ScoreSerial> scores = new List<ScoreSerial>();
        for (int i = 0; i < 10; i++)
        {
            string playerName = "Guest" + Random.Range(1, 100);
            int score = Random.Range(0, 15000);
            string date = System.DateTime.Now.AddDays(-Random.Range(0, 30)).ToString("yyyy-MM-dd");
            scores.Add(new ScoreSerial(playerName, score, date));
        }

        using StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine("PlayerName,Score,Date");
        foreach (var record in scores)
        {
            writer.WriteLine($"{record.playerName},{record.score},{record.date}");
        }
    }
}
