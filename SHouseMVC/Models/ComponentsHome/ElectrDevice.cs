namespace SHouseMVC
{
    public abstract class ElectrDevice
    {
        private bool onOff;
        private string img;

        public ElectrDevice(bool onOff, string img)
        {
            Img = img;
            OnOff = onOff;
        }

        public ElectrDevice(bool onOff)
        {
            OnOff = onOff;
        }

        public ElectrDevice() { }

        public int Id { get; set; }
        public string Img
        {
            get { return img; }
            set { img = value; }
        }

        public bool OnOff
        {
            get { return onOff; }
            set { onOff = value; }
        }

        public virtual void TurnOn()
        {
            OnOff = true;
        }

        public virtual void TurnOff()
        {
            OnOff = false;
        }
    }
}
