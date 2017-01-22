using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.SoundManagerNamespace{

	public class SoundsManager : MonoBehaviour {

		public AudioSource[] SoundAudioSources;
		public AudioSource[] MusicAudioSources;

		public int lastMusic;
		public int currentMusic;

		public void PlaySound(int index)
		{
			int count = 1;	
			while (count-- > 0)
			{
				SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
			}
		}

		public void PlayMusic(int index)
		{
			lastMusic = currentMusic;
			currentMusic = index;
			MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, false);
		}
		


		// Use this for initialization
		void Start () {
			PlayMusic (1);
			SoundManager.MusicVolume = 0.6f;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
