using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RecordsInfo : MonoBehaviour
{
    [SerializeField] private Text _recordsText;
    private string _filePath;
    private List<ScoreSerial> _scores = new ();

    private void Start()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "Records.csv");       
        LoadRecords();
        DisplayRecords();
    }

    private void LoadRecords()
    {
        _scores.Clear();

        using (StreamReader reader = new (_filePath))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                string playerName = values[0];
                int score = int.Parse(values[1]);
                string date = values[2];
                _scores.Add(new ScoreSerial(playerName, score, date));
            }
        }
        _scores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    private void DisplayRecords()
    {
        _recordsText.text = ""; 
        foreach (var record in _scores)
        {
            _recordsText.text += $"{record.playerName}: {record.score} ({record.date})\n";
        }
    }
}
