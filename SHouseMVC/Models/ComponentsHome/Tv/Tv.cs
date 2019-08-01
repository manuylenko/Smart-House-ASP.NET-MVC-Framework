using System.ComponentModel.DataAnnotations.Schema;

namespace SHouseMVC
{
    public class Tv : ElectrDevice, ISwitchVolume, ISwitchMode
    {
        private int currentVolume;
        private Channels channel;
        private int volume;

        public Tv(Channels channel, int volume, bool onOff, string img)
            : base(onOff, img)
        {
            Channel = channel;
            Volume = volume;
        }

        public Tv() { }

        [Column("Channels")]
        public int ChannelsId
        {
            get { return (int)this.Channel; }
            set
            {
                Channel = (Channels)value;
            }
        }

        [NotMapped]
        public Channels Channel
        {
            get { return channel; }
            set
            {
                channel = value;
            }
        }

        public int CurrentVolume
        {
            get { return currentVolume; }
            set
            {
                currentVolume = value;
            }
        }

        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    volume = value;
                }
            }
        }

        public void VolumePlus()
        {
            if (OnOff)
            {
                Volume += 10;
            }
        }

        public void VolumeMinus()
        {
            if (OnOff)
            {
                Volume -= 10;
            }
        }

        public void SoundOnOff()
        {
            if (OnOff)
            {
                if (Volume != 0)
                {
                    CurrentVolume = Volume;
                    Volume = 0;
                }
                else
                {
                    Volume = CurrentVolume;
                }
            }
        }

        public void NextMode()
        {
            if (OnOff)
            {
                if ((int)Channel < System.Enum.GetValues(typeof(Channels)).Length - 1)
                {
                    Channel++;
                }
                else
                {
                    Channel = (Channels)0;
                }
            }
        }

        public void PreviousMode()
        {
            if (OnOff)
            {
                if ((int)Channel > 0)
                {
                    Channel--;
                }
                else
                {
                    Channel = (Channels)System.Enum.GetValues(typeof(Channels)).Length - 1;
                }
            }
        }
    }
}