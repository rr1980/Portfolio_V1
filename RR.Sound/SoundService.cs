using NAudio.CoreAudioApi;
using RR.Common_V1;
using System;

namespace RR.Sound
{
    public class SoundService : ISoundService
    {
        public bool ToggleMute()
        {
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            //string currVolume = "MasterPeakVolume : " + defaultDevice.AudioMeterInformation.MasterPeakValue.ToString();
            defaultDevice.AudioEndpointVolume.Mute = !defaultDevice.AudioEndpointVolume.Mute;

            return defaultDevice.AudioEndpointVolume.Mute;
        }
    }
}
