namespace SHouseMVC
{

    public class Fridge : ElectrDevice, IOpenClose, ISwitchTemp
    {
        private DeepFreeze freezer;
        private int temperature;
        private bool openClose;


        public Fridge(int temperature, DeepFreeze freezer, bool onOff, string img)
            : base(onOff, img)
        {
            Temperature = temperature;
            Freezer = freezer;
            OpenClose = false;
        }

        public Fridge(int temperature, bool onOff, string img)
            : base(onOff, img)
        {
            Temperature = temperature;
        }

        public Fridge() { }

        public int? FreezerId { get; set; }
        public virtual DeepFreeze Freezer
        {
            get { return freezer; }
            set { freezer = value; }
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
                if (value >= 4 && value <= 10)
                {
                    temperature = value;
                }
                if (value < 4)
                {
                    temperature = 4;
                }
                if (value > 10)
                {
                    temperature = 10;
                }
            }
        }

        public void TempIncrease()
        {
            if(OnOff)
            {
                Temperature += 2;
                Freezer.Temperature += 2;
                Freezer.OnOff = true;
            }
        }

        public void ReduceTemp()
        {
            if (OnOff)
            {
                Temperature -= 2;
                Freezer.Temperature -= 2;
                Freezer.OnOff = true;
            }
        }

        public void Open()
        {
            OpenClose = true;
            if (Freezer.OpenClose)
            {
                Img = "/Content/Images/fridge-open.png";
            }
            else
            {
                Img = "/Content/Images/fopen-close.png";
            }
        }

        public void Close()
        {
            OpenClose = false;
            if (Freezer.OpenClose)
            {
                Img = "/Content/Images/fclose-open.png";
            }
            else
            {
                Img = "/Content/Images/fridge-close.png";
            }
        }

        public void OpenDeepFreeze()
        {
            Freezer.Open();
            if (OpenClose)
            {
                Img = "/Content/Images/fridge-open.png";
            }
            else
            {
                Img = "/Content/Images/fclose-open.png";
            }
        }

        public void CloseDeepFreeze()
        {
            Freezer.Close();
            if (OpenClose)
            {
                Img = "/Content/Images/fopen-close.png";
            }
            else
            {
                Img = "/Content/Images/fridge-close.png";
            }
        }

        public override void TurnOn()
        {
            OnOff = true;
            Freezer.OnOff = true;
        }

        public override void TurnOff()
        {
            OnOff = false;
            Freezer.OnOff = false;
        }
    }
}