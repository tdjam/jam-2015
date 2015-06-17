using UnityEngine;
using System.Collections;
using dt;
using UnityEngine.Audio;

public class CameraSynthController : MonoBehaviour {

	public AudioMixer mainMixer;

	public AudioClip horizontalPitch;
	public AudioClip verticalPitch;

	AudioSource horizontalAudio;
	AudioSource verticalAudio;

	public float minPanRange = -50f; //Applies to vertical and horizontal panning
	public float maxPanRange = 50f; //Applies to vertical and horizontal panning

	//We are going to get min and max zoom from the camera controller script
	CameraController cameraController;
	Camera camera;

	float currentCameraSize;
	Vector3 currentPosition;
	
	void Awake () {

		cameraController = this.GetComponent<CameraController> ();
		camera = this.GetComponent<Camera> ();
		currentCameraSize = camera.orthographicSize;

		SetupAudioSource (horizontalPitch, out horizontalAudio);
		SetupAudioSource (verticalPitch, out verticalAudio);

	}

	void Update () {

		bool isZoomingOrPanning = false;

		//Check if camera is panning
		if (currentPosition != transform.localPosition) {
			isZoomingOrPanning = true;
			currentPosition = transform.localPosition;

			float neutralPoint = ( maxPanRange + minPanRange ) / 2;
			float range = maxPanRange - minPanRange;

			//For vertical pitch 
			if (transform.localPosition.z > neutralPoint - (range * 0.25f) && transform.localPosition.z < neutralPoint + (range * 0.25f)) {
				//set pitch to unison
				verticalAudio.pitch = 1f;
			} else if (transform.localPosition.z >= maxPanRange) {
				//set pitch to third above
				verticalAudio.pitch = 1.1892075f;
			} else if (transform.localPosition.z <= minPanRange) {
				//set pitch to third below
				verticalAudio.pitch = 0.7937004f;
			} else if (transform.localPosition.z > 0f) {
				//set pitch to second above
				verticalAudio.pitch = 1.0594625f;
			} else if (transform.localPosition.z < 0f) {
				//set pitch to second below
				verticalAudio.pitch = 0.8908995f;
			}

//			//For horizontal pitch 
			if (transform.localPosition.x > neutralPoint - (range * 0.25f) && transform.localPosition.x < neutralPoint + (range * 0.25f)) {
				//set pitch to unison
				horizontalAudio.pitch = 1f;
			} else if (transform.localPosition.x >= maxPanRange) {
				//set pitch to third above
				horizontalAudio.pitch = 1.1892075f;
			} else if (transform.localPosition.x <= minPanRange) {
				//set pitch to third below
				horizontalAudio.pitch = 0.7937004f;
			} else if (transform.localPosition.x > 0f) {
				//set pitch to second above
				horizontalAudio.pitch = 1.1224624f;
			} else if (transform.localPosition.x < 0f) {
				//set pitch to second below
				horizontalAudio.pitch = 0.8908995f;
			}

		}

		//Check if camera is zooming
		if (currentCameraSize != camera.orthographicSize) {

			isZoomingOrPanning = true;
			currentCameraSize = camera.orthographicSize;
			float newVolume = 1f - ( (camera.orthographicSize - cameraController.minZoom) / (cameraController.maxZoom - cameraController.minZoom) );
			horizontalAudio.volume = newVolume;
			verticalAudio.volume = newVolume;

		}

		//Check if either of the above conditionals is true
		if (isZoomingOrPanning) {
			mainMixer.TransitionToSnapshots (new AudioMixerSnapshot[] {mainMixer.FindSnapshot ("CameraSynth")}, new float[] {1f}, 0.5f);
		} else {
			mainMixer.TransitionToSnapshots (new AudioMixerSnapshot[] {mainMixer.FindSnapshot ("Game")}, new float[] {1f}, 1f);
		}

	}

	void SetupAudioSource(AudioClip audio, out AudioSource audioSource) {

		GameObject audioObject = Instantiate (new GameObject());
		audioObject.transform.SetParent(this.transform);
		audioObject.name = audio.name;
	
		audioSource = audioObject.AddComponent<AudioSource> ();
		
		audioSource.loop = true;
		audioSource.clip = audio;

		audioSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("CameraSynth")[0];
		audioSource.Play ();

	}

}
