using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Moves _moves;
    [SerializeField] private Points _points;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Text _finalPoints;
    [SerializeField] private string _playerName;
    private string _filePath;
    private List<ScoreSerial> _scores = new();

    private void Start()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "Records.csv");
        _moves.MovesEnded += ShowFinalScreen;
    }

    public void EarlyEnd()
    {
        ShowFinalScreen();
    }

    private void ShowFinalScreen()
    {
        CheckPoints();
        _screen.SetActive(true);
        _finalPoints.text = _points.EarnedPoints.ToString();
    }

    private void CheckPoints()
    {
        LoadRecords();
        var minRecord = _scores.OrderBy(record => record.score).First();
        if (_points.EarnedPoints > minRecord.score)
        {
            _scores.Remove(minRecord);
            AddNewRecord(_playerName, _points.EarnedPoints);
            SaveRecords();

            SceneManager.LoadScene("Records");
        }    
    }

    private void LoadRecords()
    {
        _scores.Clear();
        using StreamReader reader = new(_filePath);
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

    private void AddNewRecord(string playerName, int score)
    {
        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
        ScoreSerial newRecord = new(playerName, score, date);
        _scores.Add(newRecord);
    }

    private void SaveRecords()
    {
        using StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine("PlayerName,Score,Date");

        foreach (var record in _scores)
        {
            writer.WriteLine($"{record.playerName},{record.score},{record.date}");
        }
    }
}
