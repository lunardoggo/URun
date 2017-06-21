using System;
using System.IO;
using UnityEngine.SceneManagement;

public static class CmdRun {

    //Array der Scene-Indicies, die beim Laden die Statistik speichern sollen
    private static readonly int[] statisticsSaveIndicies = new int[] { 0 };
    private static readonly string statisticsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cmd_Run" + Path.DirectorySeparatorChar + "statistics.bin");

    private static PlayerStats statistics = null;

    static CmdRun()
    {
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        SceneManager.sceneLoaded += OnSceneLoaded;

        ReadStatistics();
    }

    private static void ReadStatistics()
    {
        string dirPath = Path.GetDirectoryName(statisticsFilePath);
        if(!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        FileStream stream = new FileStream(statisticsFilePath, FileMode.OpenOrCreate);
        if (!ObjectSerializer.Instance.TryDeserialize(stream, out statistics))
        {
            statistics = new PlayerStats();
        }
        stream.Close();
    }

    private static void OnProcessExit(object sender, EventArgs e)
    {
        SaveStatistics();
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveStatistics();
    }

    private static void SaveStatistics()
    {
        if(statistics != null)
        {
            FileStream stream = new FileStream(statisticsFilePath, FileMode.OpenOrCreate);
            ObjectSerializer.Instance.Serialize(stream, statistics);
            stream.Close();
        }
    }

    public static PlayerStats PlayerStatistics
    {
        get { return statistics; }
    }
}
