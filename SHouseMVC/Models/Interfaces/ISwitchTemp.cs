namespace SHouseMVC
{
    public interface ISwitchTemp
    {
        int Temperature { get; set; }

        void TempIncrease();
        void ReduceTemp();
    }
}
