using System.Collections;
using UnityEngine;

namespace SoundSystem
{
    public static class AudioSourceExtensions
    {
        public static void Play(this AudioSource audioSource, AudioClip audioClip = null, float volum = 1f)
        {
            if (audioClip != null)
            {
                audioSource.clip = audioClip;

                // �{�����[�����K�؂Ȃ��l�ɂȂ�悤�ɒ���
                audioSource.volume = Mathf.Clamp01(volum);

                audioSource.Play();
            }
        }

        public static IEnumerator PlayRandomStart(this AudioSource audioSource, AudioClip audioClip = null, float volum = 1f)
        {
            if (audioClip == null) { yield break; }

            audioSource.clip = audioClip;
            audioSource.volume = Mathf.Clamp01(volum);

            // ���ʂ� length �𓯂��ɂȂ�ƃV�[�N�G���[���N�������߁@-0.01�b����
            audioSource.time = Random.Range(0f, audioClip.length - 0.01f);

            //yield return PlayWithFadeIn(audioSource, audioClip, volum);
        }

        public static IEnumerator PlayWithFadeIn(this AudioSource audioSource, AudioClip audioClip = null, float fadeTime = 0.1f, float endVolume = 1.0f)
        {
            // �ڕW�{�����[����0����1�C��
            float targetVolume = Mathf.Clamp01(endVolume);

            // �t�F�[�h���Ԃ��������������ꍇ�͕␳
            fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

            // ����0�ōĐ��J�n
            audioSource.Play(audioClip, 0f);

            for (float t = 0f; t < fadeTime; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(0f, targetVolume, Mathf.Clamp01(t / fadeTime));
                yield return null;
            }
            audioSource.volume = targetVolume;
        }

        public static IEnumerator StopWithFadeOut(this AudioSource audioSource, float fadeTime = 0.1f)
        {
            float startVolume = audioSource.volume;

            // �t�F�[�h���Ԃ��������������ꍇ�͕␳
            fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

            for (float t= 0f; t < fadeTime; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, Mathf.Clamp01(t / fadeTime));
                yield return null;
            }
            audioSource.volume = 0f;

            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}