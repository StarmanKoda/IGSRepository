using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeAdjuster : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        if(music == null)
        {
            music = GetComponent<AudioSource>();
        }
        if(music == null)
        {
            Destroy(this); return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = ZoneLoader.zoneLoader.master_volume * ZoneLoader.zoneLoader.music_volume;
    }
}
