using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public enum BGMList
{
    Title,
    Main
}

public enum SEList
{
    Select,
    Decision,
    Run,
    Push,
    Cancel,
    PressureSensitive,
    Teleport,
    Up,
    Beam
}

namespace SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                    if (instance == null)
                    {
                        Debug.LogError(typeof(SoundManager) + "is nothing");
                    }
                }
                return instance;
            }
        }

        // BGM・SE・VoiceのAudioClipリスト
        public List<AudioClip> bgmAudioClipList = new List<AudioClip>();
        public List<AudioClip> seAudioClipList = new List<AudioClip>();

        [SerializeField, Header("Audio Mixer")]
        public AudioMixer audioMixer;
        public AudioMixerGroup bgmAMG, seAMG;

        // BGM・SE・Voiceの名AudioSource
        List<AudioSource> bgmAudioSourceList = new List<AudioSource>();
        AudioSource seAudioSource;

        List<IEnumerator> fadeCoroutines = new List<IEnumerator>();

        const int BGMAudioSourceNum = 2;
        const string MasterVolumeParamName = "MasterVolume";
        const string BGMVolumeParamName = "BGMVolume";
        const string SEVolumeParamName = "SEVolume";

        // 一時停止中か
        public bool IsPaused { get; private set; }

        public float MasterVolume
        {
            get { return audioMixer.GetVolumeByLinear(MasterVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(MasterVolumeParamName, value); }
        }
        public float SEVolume
        {
            get { return audioMixer.GetVolumeByLinear(SEVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(SEVolumeParamName, value); }
        }
        public float BGMVolume
        {
            get { return audioMixer.GetVolumeByLinear(BGMVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(BGMVolumeParamName, value); }
        }

        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);

            seAudioSource = InitializeAudioSource(gameObject, false, seAMG);
            bgmAudioSourceList = InitializeAudioSources(gameObject, true, bgmAMG, BGMAudioSourceNum);

            IsPaused = false;
        }

        private List<AudioSource> InitializeAudioSources(GameObject parentGameObject, bool isLoop = false, AudioMixerGroup amg = null, int count = 1)
        {
            List<AudioSource> audioSources = new List<AudioSource>();

            for (int i = 0; i < count; i++)
            {
                var audioSource = InitializeAudioSource(parentGameObject, isLoop, amg);
                audioSources.Add(audioSource);
            }

            return audioSources;
        }

        private AudioSource InitializeAudioSource(GameObject parentGameObject, bool isLoop = false, AudioMixerGroup amg = null, int bGMAudioSourceNum = 0)
        {
            var audioSource = parentGameObject.AddComponent<AudioSource>();

            audioSource.loop = isLoop;
            audioSource.playOnAwake = false;

            if (amg != null)
            {
                audioSource.outputAudioMixerGroup = amg;
            }

            return audioSource;
        }

        public bool IsSEPlay()
        {
            return seAudioSource.isPlaying;
        }

        public void PlayOneShotSe(int clipNum)
        {
            var audioClip = seAudioClipList[clipNum];

            if (audioClip == null)
            {
                Debug.LogError(clipNum + "は見つかりません");
                return;
            }

            seAudioSource.PlayOneShot(audioClip);
        }

        public void PlayShotSe(int clipNum)
        {
            var audioClip = seAudioClipList[clipNum];

            if (audioClip == null)
            {
                Debug.LogError(clipNum + "は見つかりません");
                return;
            }

            seAudioSource.Play(audioClip);
        }

        public void StopSE()
        {
            seAudioSource.Stop();
        }
      
        public void PlayBGMWithFadeIn(int clipNum, float fadeTime = 2f)
        {
            if (IsPaused) { return; }

            var audioClip = bgmAudioClipList[clipNum];

            if (audioClip == null)
            {
                Debug.LogError(clipNum + "は見つかりません");
                return;
            }

            if (bgmAudioSourceList.Any(source => source.clip == audioClip))
            {
                Debug.LogError(clipNum + "はすでに再生されています");
                return;
            }

            StopBGMWithFadeOut(fadeTime); // 現在再生中のBGMをフェードアウトする

            AudioSource audioSource = bgmAudioSourceList[clipNum];

            if (audioSource != null)
            {
                IEnumerator routine = audioSource.PlayWithFadeIn(audioClip, fadeTime);
                fadeCoroutines.Add(routine);
                StartCoroutine(routine);
            }
        }

        public void StopBGMWithFadeOut(int clipNum, float fadeTime)
        {
            if (IsPaused) { return; }

            AudioSource audioSource = bgmAudioSourceList[clipNum];
            
            if (audioSource == null || audioSource.isPlaying == false)
            {
                Debug.LogError(clipNum + "は再生されていません");
                return;
            }

            IEnumerator routine = audioSource.StopWithFadeOut(fadeTime);
            StartCoroutine(routine);
            fadeCoroutines.Add(routine);
        }

        public void StopBGMWithFadeOut(float fadeTime = 2f)
        {
            if (IsPaused) { return; }

            fadeCoroutines.ForEach(StopCoroutine);
            fadeCoroutines.Clear();

            fadeCoroutines = bgmAudioSourceList.Where(asb => asb.isPlaying)
                .ToList()
                .ConvertAll(asb =>
                {
                    IEnumerator routine = asb.StopWithFadeOut(fadeTime);
                    StartCoroutine(routine);
                    return routine;
                });
        }
    }
}