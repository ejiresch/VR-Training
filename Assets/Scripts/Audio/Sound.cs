using UnityEngine;
using System.Collections;

namespace development_a {
    [System.Serializable]
    public class Sound {
        public string name; // Name des jeweiligen Sounds
        public AudioClip clip; // Clip des jeweiligen Sounds

        // Lautstaerke des jeweiligen Sounds von 0.0 bis 1.0
        [Range(0f, 1f)] public float volume;

        // Pitch des jeweiligen Sounds
        /*
        [Range(-1f, 3f)] public float pitch;
        */

        // Ob sich der Sound loopen soll
        public bool loop;

        /*[HideInInspector]*/public AudioSource source;
        

        /*
         * Spielt den Sound ab. Aber smoothly.
         */
        public IEnumerator SmoothPlay(float seconds) {
            if (!source.isPlaying) {
                source.volume = 0f;
                source.Play();
                for (int i = 0; i < 20; i++) {
                    source.volume += volume / 20.0f;

                    yield return new WaitForSeconds(seconds / 20.0f);
                }
            }
        }

        /*
         * Haelt den Sound an. Aber smoothly.
         */
        public IEnumerator SmoothStop(float seconds) {
            float currentVolume = source.volume;
            if (source.isPlaying) {
                for (int i = 0; i < 20; i++) {
                    source.volume -= currentVolume / 20.0f;

                    yield return new WaitForSeconds(seconds / 20.0f);
                }

                source.Stop();
            }
        }
    }
}