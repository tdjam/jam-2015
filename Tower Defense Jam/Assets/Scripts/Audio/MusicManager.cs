using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Music {

	[System.Serializable]
	public class Track {
		public string id;
		public AudioClip clip;
		public string mixerGroup;
		public bool loop = true;
		public bool playOnAwake = false;
	}
	
	public class MusicManager : MonoBehaviour {
		
		[SerializeField] public AudioMixer mainMixer;
		[SerializeField] string defaultAutoPlayId;
		[SerializeField] List<Track> tracks;

		void Awake () {

			//Create an audio object for each track
			foreach (Track t in tracks) {

				GameObject prefab = Instantiate (new GameObject());
				prefab.transform.SetParent(this.transform);
				prefab.name = t.id;

				//Set values for the audio source component
				AudioSource audioPrefab = prefab.AddComponent<AudioSource>();

				audioPrefab.loop = t.loop;
				audioPrefab.playOnAwake = false;
				audioPrefab.clip = t.clip;

				if (mainMixer.FindMatchingGroups(t.mixerGroup).Length > 0)
					audioPrefab.outputAudioMixerGroup = mainMixer.FindMatchingGroups(t.mixerGroup)[0];

				if (t.playOnAwake || t.id == defaultAutoPlayId) audioPrefab.Play();

			}
			
		}

		//This function allows you to change between snapshots
		public void transitionAudioSnapshot(string snapshot, float transitionTime) {
			mainMixer.FindSnapshot (snapshot).TransitionTo (transitionTime);
		}

		//Play all tracks in a particular group
		public void playAudioGroup (string group) {

			foreach (Track t in tracks) {
				if (t.mixerGroup == group) {
					this.transform.FindChild(t.id).gameObject.GetComponent<AudioSource>().Play();
				}
			}

		}

		//Stop all tracks in a particular group OR stop all tracks in all groups EXCEPT the defined group
		public void StopAudioGroup (string group, float delay, bool stopAllExceptDefinedGroup) {
			StartCoroutine (StopAudioLoop (group, delay, stopAllExceptDefinedGroup));
		}

		IEnumerator StopAudioLoop (string group, float delay, bool stopAllExceptDefinedGroup) {
			yield return new WaitForSeconds(delay);

			foreach (Track t in tracks) {

				if ( (stopAllExceptDefinedGroup && t.mixerGroup != group) || t.mixerGroup == group ) {
					this.transform.FindChild(t.id).gameObject.GetComponent<AudioSource>().Stop();
				}

			}	
		}

		//Combines the above three functions so you can switch snapshots and transition groups that are playing
		public void TransitionAudioSnapshotsAndGroups(string snapshot) {
			float transitionTime = 1f;
			transitionAudioSnapshot (snapshot, transitionTime);
			playAudioGroup (snapshot);
			StopAudioGroup (snapshot, transitionTime, true);
		}

		//Allows you to play an individual track
		public void PlayTrack(string track) {
			this.transform.FindChild(track).gameObject.GetComponent<AudioSource>().Play();
		}

		//Allows you to stop an individual track
		public void StopTrack(string track) {
			this.transform.FindChild(track).gameObject.GetComponent<AudioSource>().Stop();
		}

	}


}