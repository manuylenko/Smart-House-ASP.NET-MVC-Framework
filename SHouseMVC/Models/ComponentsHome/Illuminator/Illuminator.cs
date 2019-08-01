using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHouseMVC
{
    public class Illuminator : ElectrDevice
    {
        private BrightnesMode brightness;

        public Illuminator(BrightnesMode brightness, bool onOff, string img)
            : base(onOff, img)
        {
            Brightness = brightness;
        }

        public Illuminator() { }

        [Column("Brightness")]
        public int BrightnessId
        {
            get { return (int)this.Brightness; }
            set { this.Brightness = (BrightnesMode)value; }
        }

        [NotMapped]
        public BrightnesMode Brightness
        {
            get { return brightness; }
            set { brightness = value; }
        }

        public void SetLowBrightness()
        {
            OnOff = true;
            Brightness = BrightnesMode.Low;
            Img = "/Content/Images/low.png";
        }

        public void SetMediumBrightness()
        {
            OnOff = true;
            Brightness = BrightnesMode.Medium;
            Img = "/Content/Images/medium.png";
        }

        public void SetHighBrightness()
        {
            OnOff = true;
            Brightness = BrightnesMode.High;
            Img = "/Content/Images/high.png";
        }

        public override void TurnOn()
        {
            OnOff = true;
            SetLowBrightness();
        }

        public override void TurnOff()
        {
            OnOff = false;
            Brightness = BrightnesMode.None;
            Img = "/Content/Images/off-lamp.png";
        }
    }
}