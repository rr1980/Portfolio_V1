using Microsoft.Extensions.Logging;
using NAudio.CoreAudioApi;
using RR.Common_V1;
using System;

namespace RR.Sound
{
    public class SoundService : ISoundService
    {
        private readonly ILogger<SoundService> _logger;
        private readonly MMDevice _defaultDevice;

        public SoundService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SoundService>();
            _defaultDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _logger.LogInformation("SoundService created!");
        }

        public (bool, int) ToggleMute()
        {
            _defaultDevice.AudioEndpointVolume.Mute = !_defaultDevice.AudioEndpointVolume.Mute;
            return GetVolumeInPercent();
        }

        public (bool, int) GetVolumeInPercent()
        {
            var (Mute, Volume) = (_defaultDevice.AudioEndpointVolume.Mute,(int)(_defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100f));
            return (Mute, Volume);
        }

        public (bool, int) VolumeStepDown()
        {
            _defaultDevice.AudioEndpointVolume.VolumeStepDown();
            return GetVolumeInPercent();
        }

        public (bool, int) VolumeStepUp()
        {
            _defaultDevice.AudioEndpointVolume.VolumeStepUp();
            return GetVolumeInPercent();
        }
    }
}