namespace SHouseMVC
{
    public class DeepFreeze : ElectrDevice, IOpenClose
    {
        private int temperature;
        private bool openClose;


        public DeepFreeze(int temperature, bool onOff)
            : base(onOff)
        {
            Temperature = temperature;
            OpenClose = false;
        }

        public DeepFreeze() { }

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
                if (value <= -18 && value >= -24)
                {
                    temperature = value;
                }
                if (value > -18)
                {
                    temperature = -18;
                }
                if (value < -24)
                {
                    temperature = -24;
                }
            }
        }

        public void Open()
        {
            OpenClose = true;
        }

        public void Close()
        {
            OpenClose = false;
        }
    }
}