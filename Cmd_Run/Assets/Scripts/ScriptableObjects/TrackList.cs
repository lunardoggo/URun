using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tracklist", menuName = "Cmd_Run/Tracklist", order = 2)]
public class TrackList : ScriptableObject
{

    [SerializeField]
    private List<AudioClip> tracks = null;

    private List<AudioClip> lastlyPlayed = null;
    private Random rnd = new Random();

    private void OnEnable()
    {
        if (tracks == null)
        {
            tracks = new List<AudioClip>();
        }
        lastlyPlayed = new List<AudioClip>();
    }

    /// <summary>
    /// Returns a random <see cref="AudioClip"/> out of this <see cref="TrackList"/>
    /// </summary>
    public AudioClip GetRandom()
    {
        AudioClip clip = null;
        int count = 0;
        do
        {
            clip = tracks[Random.Range(0, Count)];
            //Emergency-break Variable
            count++;
        } while (lastlyPlayed.Contains(clip) && count < 100);
        lastlyPlayed.Add(clip);

        while (lastlyPlayed.Count > Mathf.Clamp(Count - 1, 0, Count))
        {
            lastlyPlayed.RemoveAt(0);
        }

        return clip;
    }

    /// <summary>
    /// Returns the count of the currently loaded <see cref="AudioClip"/>s
    /// </summary>
    public int Count
    {
        get { return tracks.Count; }
    }
}
