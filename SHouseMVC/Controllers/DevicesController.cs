using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Net;

namespace SHouseMVC
{
    public class DevicesController : Controller
    {

        private DevicesContext db = new DevicesContext();
        List<ElectrDevice> g;

        public ActionResult Index()
        {

            return View(ListDevices(db));
        }

        public ActionResult Create(string parametr)
        {
            switch (parametr)
            {
                case "Illuminator":
                    db.Illuminators.Add(new Illuminator(BrightnesMode.None, false, "/Content/Images/off-lamp.png"));
                    break;
                case "Alarm":
                    db.Alarms.Add(new Alarm(AlarmMode.Expectation, false, "/Content/Images/alarm-off.png"));
                    break;
                case "Fridge":
                    db.Fridges.Add(new Fridge(4, new DeepFreeze(-24, false), false, "/Content/Images/fridge-close.png"));
                    break;
                case "Microwave":
                    db.Microwaves.Add(new Microwave(160, false, false, "/Content/Images/icon/micrrocloseoff.png"));
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            switch (id)
            {
                case "Illuminator":
                    device = db.Illuminators.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    db.Illuminators.Remove((Illuminator)device);
                    break;
                case "Alarm":
                    device = db.Alarms.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    db.Alarms.Remove((Alarm)device);
                    break;
                case "Fridge":
                    db.Fridges.Remove(db.Fridges.Find(parametr));
                    db.DeepFreezes.Remove(db.DeepFreezes.Find(parametr));
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    db.Microwaves.Remove((Microwave)device);
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TurnOffOn(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            switch (id)
            {
                case "Illuminator":
                    device = db.Illuminators.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    if (device.OnOff)
                    {
                        device.TurnOff();
                        ((Illuminator)device).Img = "/Content/Images/off-lamp.png";
                    }
                    else
                    {
                        device.TurnOn();
                        ((Illuminator)device).Img = "/Content/Images/low.png";
                    }
                    db.Entry((Illuminator)device).State = EntityState.Modified;
                    break;
                case "Alarm":
                    device = db.Alarms.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    if (device.OnOff)
                    {
                        device.TurnOff();
                        ((Alarm)device).Img = "/Content/Images/alarm-off.png";
                    }
                    else
                    {
                        device.TurnOn();
                        ((Alarm)device).Img = "/Content/Images/alarm-no.png";
                    }
                    db.Entry((Alarm)device).State = EntityState.Modified;
                    break;
                case "Fridge":
                    device = db.Fridges.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    if (device.OnOff)
                    {
                        device.TurnOff();
                    }
                    else
                    {
                        device.TurnOn();
                    }
                    db.Entry((Fridge)device).State = EntityState.Modified;
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    if (device.OnOff)
                    {
                        device.TurnOff();
                        if (((Microwave)device).OpenClose)
                        {
                            ((Microwave)device).Img = "/Content/Images/icon/microwaveoffop.png";
                        }
                        else
                        {
                            ((Microwave)device).Img = "/Content/Images/icon/micrrocloseoff.png";
                        }                  
                    }
                    else
                    {
                        device.TurnOn();
                        if (((Microwave)device).OpenClose)
                        {
                            ((Microwave)device).Img = "/Content/Images/microwave-open.png";
                        }
                        else
                        {
                            ((Microwave)device).Img = "/Content/Images/microwave-close.png";
                        }
                    }
                    db.Entry((Microwave)device).State = EntityState.Modified;
                    break;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NextMode(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            device = db.Microwaves.Find(parametr);
            if (device == null) { return HttpNotFound(); }
            ((Microwave)device).NextMode();
            db.Entry(((Microwave)device)).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PreviousMode(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;
            device = db.Microwaves.Find(parametr);
            if (device == null) { return HttpNotFound(); }
            ((Microwave)device).PreviousMode();
            db.Entry(((Microwave)device)).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Close(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            switch (id)
            {
                case "Fridge":
                    device = db.Fridges.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    ((Fridge)device).Close();
                    if (((Fridge)device).Freezer.OpenClose)
                    {
                        ((Fridge)device).Img = "/Content/Images/fclose-open.png";
                    }
                    else
                    {
                        ((Fridge)device).Img = "/Content/Images/fridge-close.png";
                    }
                    db.Entry((Fridge)device).State = EntityState.Modified;
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    ((Microwave)device).Close();
                    if (((Microwave)device).OnOff)
                    {
                        ((Microwave)device).Img = "/Content/Images/microwave-close.png";
                    }
                    else
                    {
                        ((Microwave)device).Img = "/Content/Images/icon/micrrocloseoff.png";
                    }
                    db.Entry(((Microwave)device)).State = EntityState.Modified;
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Open(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            switch (id)
            {
                case "Fridge":
                    device = db.Fridges.Find(parametr);
                    ((Fridge)device).Open();
                    if (((Fridge)device).Freezer.OpenClose)
                    {
                        ((Fridge)device).Img = "/Content/Images/fridge-open.png";
                    }
                    else
                    {
                        ((Fridge)device).Img = "/Content/Images/fopen-close.png";
                    }
                    db.Entry((Fridge)device).State = EntityState.Modified;
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    ((Microwave)device).Open();
                    if (((Microwave)device).OnOff)
                    {
                        ((Microwave)device).Img = "/Content/Images/microwave-open.png";
                    }
                    else
                    {
                        ((Microwave)device).Img = "/Content/Images/icon/microwaveoffop.png";
                    }
                    db.Entry(((Microwave)device)).State = EntityState.Modified;
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReduceTemp(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device;

            switch (id)
            {
                case "Fridge":
                    device = db.Fridges.Find(parametr);
                    ((Fridge)device).ReduceTemp();
                    db.Entry((Fridge)device).State = EntityState.Modified;
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    ((Microwave)device).ReduceTemp();
                    db.Entry(((Microwave)device)).State = EntityState.Modified;
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TempIncrease(int? parametr, string id)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectrDevice device = null;

            switch (id)
            {
                case "Fridge":
                    device = db.Fridges.Find(parametr);
                    ((Fridge)device).TempIncrease();
                    db.Entry((Fridge)device).State = EntityState.Modified;
                    break;
                case "Microwave":
                    device = db.Microwaves.Find(parametr);
                    if (device == null) { return HttpNotFound(); }
                    ((Microwave)device).TempIncrease();
                    db.Entry(((Microwave)device)).State = EntityState.Modified;
                    break;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetDisarmed(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarm alarm = db.Alarms.Find(parametr);
            if (alarm == null) { return HttpNotFound(); }
            alarm.SetDisarmed();
            alarm.Img = "/Content/Images/alarm-no.png";
            db.Entry(alarm).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetIsArmed(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarm alarm = db.Alarms.Find(parametr);
            if (alarm == null) { return HttpNotFound(); }
            alarm.SetIsArmed();
            alarm.Img = "/Content/Images/alarm-on.png";
            db.Entry(alarm).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetNobodyHome(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarm alarm = db.Alarms.Find(parametr);
            if (alarm == null) { return HttpNotFound(); }
            alarm.SetNobodyHome(ListDevices(db));
            foreach(var device in ListDevices(db))
            {
                if(device is Illuminator)
                {
                    ((Illuminator)device).Img = "/Content/Images/off-lamp.png";
                }
                else if (device is Microwave)
                {
                    if (((Microwave)device).OpenClose)
                        ((Microwave)device).Img = "/Content/Images/icon/micrrocloseoff.png";
                        ((Microwave)device).Img = "/Content/Images/icon/microwaveoffop.png";
                }
            }
            alarm.Img = "/Content/Images/alarm-super.png";


            foreach (var sd in ListDevices(db))
            {
                db.Entry(sd).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CloseDeepFreeze(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridge fridge = db.Fridges.Find(parametr);

            if (fridge == null) { return HttpNotFound(); }

            fridge.CloseDeepFreeze();
            if (fridge.OpenClose)
            {
                fridge.Img = "/Content/Images/fopen-close.png";
            }
            else
            {
                fridge.Img = "/Content/Images/fridge-close.png";
            }

            db.Entry(fridge).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OpenDeepFreeze(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridge fridge = db.Fridges.Find(parametr);

            if (fridge == null) { return HttpNotFound(); }

            fridge.OpenDeepFreeze();
            if (fridge.OpenClose)
            {
                fridge.Img = "/Content/Images/fridge-open.png";
            }
            else
            {
                fridge.Img = "/Content/Images/fclose-open.png";
            }

            db.Entry(fridge).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetLowBrightness(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illuminator illuminator = db.Illuminators.Find(parametr);
            if (illuminator == null) { return HttpNotFound(); }

            ((Illuminator)illuminator).SetLowBrightness();
            illuminator.Img = "/Content/Images/low.png";
            db.Entry(illuminator).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetMediumBrightness(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illuminator illuminator = db.Illuminators.Find(parametr);
            if (illuminator == null) { return HttpNotFound(); }

            ((Illuminator)illuminator).SetMediumBrightness();
            illuminator.Img = "/Content/Images/medium.png";
            db.Entry(illuminator).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetHighBrightness(int? parametr)
        {
            if (parametr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illuminator illuminator = db.Illuminators.Find(parametr);
            if (illuminator == null) { return HttpNotFound(); }

            ((Illuminator)illuminator).SetHighBrightness();

            illuminator.Img = "/Content/Images/high.png";

            db.Entry(illuminator).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<ElectrDevice> ListDevices(DevicesContext db)
        {
            var microwaves = db.Microwaves.ToList();
            var alarms = db.Alarms.ToList();
            var fridges = db.Fridges.Include(f => f.Freezer).ToList();
            var illuminators = db.Illuminators.ToList();
            var tvs = db.Tvs.ToList();


            g = new List<ElectrDevice>();

            foreach (var item in microwaves)
            {
                g.Add(item);
            }

            foreach (var item in alarms)
            {
                g.Add(item);
            }

            foreach (var item in fridges)
            {
                g.Add(item);
            }

            foreach (var item in illuminators)
            {
                g.Add(item);
            }

            foreach (var item in tvs)
            {
                g.Add(item);
            }
            return g;
        }
    }
}