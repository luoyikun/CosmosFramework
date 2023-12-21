﻿namespace Cosmos.Audio
{
    /// <summary>
    /// 声音播放帮助体；
    /// </summary>
    public interface IAudioPlayHelper
    {
        bool Mute { get; set; }
        void PlayAudio(AudioObject audioObject,AudioParams audioParams, AudioPositionParams audioPlayInfo);
        void StopAudio(AudioObject audioObject,float fadeTime);
        void PauseAudio(AudioObject audioObject, float fadeTime);
        void ResumeAudio(AudioObject audioObject, float fadeTime);
        void SetAudioParam(AudioObject audioObject, AudioParams audioParams);
        void ClearAllAudio();
        void TickRefresh();
    }
}
