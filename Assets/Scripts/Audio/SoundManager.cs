using System;
using UnityEngine;
using UnityEngine.Audio;


namespace development_a {
    /**
     * Die Klasse Soundmanager dient dazu alle moeglichen Sounds dieser Applikation zu verwalten.
     * 
     * Diese Klasse wurde mit Hilfe des Tutorials von Brackeys erstellt.
     * Link zum Tutorial: https://www.youtube.com/watch?v=6OT43pvUyfY
     *
     * @author Firat Adlim
     * @version 2021-10-18
     */
    public class SoundManager : MonoBehaviour {
        public Sound[] sounds;
        public static SoundManager instance;

        /**
         * Awake wird bevor die Applikation losstartet ausgefuehrt, um die Variablen und sonstiges davor noch zu
         * initialisieren.
         * Hier wird abgecheckt, ob ein SoundManager in der Applikation schon existiert. Wenn nicht bzw. wenn die
         * Applikation gerade gestartet wurde, dann wird dieser SoundManager als aktueller SoundManager der ganzen
         * Applikation gesetzt.
         */
        private void Awake() {
            if (instance == null) instance = this;
            else {
                Destroy(gameObject);
                return;
            }

            //DontDestroyOnLoad(gameObject);

            foreach (Sound sound in sounds) {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;
            }
        }

        void Start() {
            // ManageSound("taskdone", true, 0);
        }

        /**
         * Durch Angabe des Namens des gewuenschten Sounds wird dieser dementsprechend auch entweder abgespielt oder
         * gestoppt. Des Weiteren kann angegeben werden, ob der Sound smooth abgespielt werden soll oder nicht.
         */
        public void ManageSound(string name, bool play, float smoothnessSeconds) {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null) {
                Debug.LogError("Sound with given name " + name + "not found");
                return;
            }
            
            if (smoothnessSeconds > 0) {
                if (play) StartCoroutine(s.SmoothPlay(smoothnessSeconds));
                if (!play) StartCoroutine(s.SmoothStop(smoothnessSeconds));
            }
            else if (smoothnessSeconds == 0) {
                if (play) s.source.Play();
                if (!play) s.source.Stop();
            }

            //Debug.Log("SOUND");
        }
    }
}