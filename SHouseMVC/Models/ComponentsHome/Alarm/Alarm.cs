using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHouseMVC
{
    public class Alarm : ElectrDevice
    {
        private AlarmMode modeSignal;

        public Alarm(AlarmMode modeSignal, bool onOff, string img) : base(onOff, img) 
        {
            ModeSignal = modeSignal;
        }

        public Alarm() { }

        [Column("ModeSignal")]
        public int ModeSignalId
        {
            get { return (int)modeSignal; }
            set { modeSignal = (AlarmMode)value; }
        }

        [NotMapped]
        public AlarmMode ModeSignal 
        {
            get { return modeSignal; }
            set { modeSignal = value; }
        }

        public void SetIsArmed()
        {
            if(OnOff)
            {
                ModeSignal = AlarmMode.Protected;
                Img = "/Content/Images/alarm-on.png";
            }
        }

        public void SetDisarmed()
        {
            if (OnOff)
            {
                ModeSignal = AlarmMode.Expectation;
                Img = "/Content/Images/alarm-no.png";
            }
        }

        public void SetNobodyHome(List<ElectrDevice> devices)
        {
            if (OnOff)
            {
                foreach (var device in devices)
                {
                    if (!((device is Alarm) || (device is Fridge)))
                    {
                        device.TurnOff();
                    }
                    if(device is Microwave)
                    {
                        ((Microwave)device).Close();
                    }
                }
                ModeSignal = AlarmMode.Super_Home;
                Img = "/Content/Images/alarm-super.png";
            }
        }

        public override void TurnOn()
        {
            OnOff = true;
            SetDisarmed();
        }

        public override void TurnOff()
        {
            SetDisarmed();
            OnOff = false;
            Img = "/Content/Images/alarm-off.png";
        }
    }
}