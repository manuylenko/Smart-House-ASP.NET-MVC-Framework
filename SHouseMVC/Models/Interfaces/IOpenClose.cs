namespace SHouseMVC
{
    interface IOpenClose
    {
        bool OpenClose { get; set; }

        void Open();
        void Close();
    }
}