using System.Collections.Generic;
using System.Data.Entity;

namespace SHouseMVC
{
    public class Repository
    {
        private DevicesContext dbDevices;
        private List<ElectrDevice> devices;
        private DbSet dbSetDevice;

        public Repository(DevicesContext dbDevices)
        {
            this.dbDevices = dbDevices;
            devices = ToList();
        }

        public List<ElectrDevice> Devices 
        {
            get { return devices; }
            set { devices = value; }
        }

        public void Edit(ElectrDevice device)
        {
            dbDevices.Entry(device).State = EntityState.Modified;
            dbDevices.SaveChanges();
        }

        public void Remove(ElectrDevice device)
        {
            DbDevice(device);
            if (device is Fridge)
            {
                dbDevices.DeepFreezes.Remove(dbDevices.DeepFreezes.Find(device.Id));
            }
            dbSetDevice.Remove(device);
            dbDevices.SaveChanges();
        }

        public void Add(ElectrDevice device)
        {
            DbDevice(device);
            dbSetDevice.Add(device);
            dbDevices.SaveChanges();
        }

        public void DbDevice(ElectrDevice device)
        {
            if (device is Illuminator)
            {
                dbSetDevice = dbDevices.Illuminators;
            }
            else if (device is Alarm)
            {
                dbSetDevice = dbDevices.Alarms;
            }
            else if (device is Fridge)
            {
                dbSetDevice = dbDevices.Fridges;

            }
            else if (device is Tv)
            {
                dbSetDevice = dbDevices.Tvs;
            }
            else if (device is Microwave)
            {
                dbSetDevice = dbDevices.Microwaves;
            }
        }

        public List<ElectrDevice> ToList()
        {
            devices = new List<ElectrDevice>();

            foreach (var microwave in dbDevices.Microwaves)
            {
                devices.Add(microwave);
            }
            foreach (var alarm in dbDevices.Alarms)
            {
                devices.Add(alarm);
            }
            foreach (var fridge in dbDevices.Fridges)
            {
                devices.Add(fridge);
            }
            foreach (var lamp in dbDevices.Illuminators)
            {
                devices.Add(lamp);
            }
            foreach (var tv in dbDevices.Tvs)
            {
                devices.Add(tv);
            }
            return devices;
        }

        public int IdDb(ElectrDevice device)
        {
            int idDbDevice = 0;
            if (device is Illuminator) { idDbDevice = 1; }
            else if (device is Alarm) { idDbDevice = 2; }
            else if (device is Fridge) { idDbDevice = 3; }
            else if (device is Tv) { idDbDevice = 4; }
            else if (device is Microwave) { idDbDevice = 5; }
            return idDbDevice;
        }
    }
}