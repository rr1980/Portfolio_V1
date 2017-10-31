namespace RR.SoundService.Common
{
    public interface ISoundService
    {
        (bool, int) ToggleMute();
        (bool, int) GetVolumeInPercent();
        (bool, int) VolumeStepDown();
        (bool, int) VolumeStepUp();
    }
}
