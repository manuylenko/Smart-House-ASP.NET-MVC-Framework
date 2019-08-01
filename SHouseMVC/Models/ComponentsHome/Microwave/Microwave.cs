using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHouseMVC
{
    public class Microwave : ElectrDevice, IOpenClose, ISwitchTemp, ISwitchMode
    {
        private ModeOven cookingModes;
        private int temperature;
        private bool openClose;

        public Microwave(int temperature, bool openClose, bool onOff, string img)
            : base(onOff, img)
        {
            Temperature = temperature;
            OpenClose = openClose;
        }

        public Microwave() { }

        [Column("CookingModes")]
        public int CookingModesId
        {
            get { return (int)this.CookingModes; }
            set
            {
                if (value <= 0)
                {
                    this.CookingModes = ModeOven.Top_Bottom;
                }
                else if (value >= 4)
                {
                    this.CookingModes = ModeOven.Grill_Bottom;
                }
                else
                {
                    this.CookingModes = (ModeOven)value;
                }
            }
        }

        [NotMapped]
        public ModeOven CookingModes
        {
            get { return cookingModes; }
            set
            {
                cookingModes = value;
            }
        }
        public bool OpenClose
        {
            get { return openClose; }
            set { openClose = value; }
        }

        public int Temperature
        {
            get { return temperature; }
            set
            {
                if (value < 160)
                {
                    temperature = 160;
                }
                else if (value > 280)
                {
                    temperature = 280;
                }
                else
                {
                    temperature = value;
                }
            }
        }

        public void TempIncrease()
        {
            if (OnOff)
            {
                Temperature += 20;
            }
        }

        public void ReduceTemp()
        {
            if (OnOff)
            {
                Temperature -= 20;
            }
        }

        public void Open()
        {
            OpenClose = true;
            if (OnOff)
            {
                Img = "/Content/Images/microwave-open.png";
            }
            else
            {
                Img = "/Content/Images/icon/microwaveoffop.png";
            }
        }

        public void Close()
        {
            OpenClose = false;
            if (OnOff)
            {
                Img = "/Content/Images/microwave-close.png";
            }
            else
            {
                Img = "/Content/Images/icon/micrrocloseoff.png";
            }
        }

        public void NextMode()
        {
            if (OnOff)
            {
                if ((int)(CookingModes) < Enum.GetValues(typeof(ModeOven)).Length - 1)
                {
                    CookingModes++;
                }
                else
                {
                    CookingModes = (ModeOven)0;
                }
            }
        }

        public void PreviousMode()
        {
            if (OnOff)
            {
                if (((int)(CookingModes)) > 0)
                {
                    CookingModes--;
                }
                else
                {
                    CookingModes = (ModeOven)System.Enum.GetValues(typeof(ModeOven)).Length - 1;
                }
            }
        }

        public override void TurnOn()
        {
            OnOff = true;
            if (OpenClose)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public override void TurnOff()
        {
            OnOff = false;
            if (OpenClose)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}