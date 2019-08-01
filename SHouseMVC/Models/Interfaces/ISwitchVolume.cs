namespace SHouseMVC
{
    interface ISwitchVolume
    {
        int Volume { get; set; }

        void VolumePlus();
        void VolumeMinus();
    }
}
