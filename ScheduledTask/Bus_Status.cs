using Buses.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Buses.ScheduledTask
{
    public class Bus_Status : IJob
    {
        private Bus_Ticketing_SystemEntities db = new Bus_Ticketing_SystemEntities();
        public Task Execute(IJobExecutionContext context)
        {
            var TodaySchedules = db.Bus_Scheduling.AsNoTracking()/*.Where(o => Convert.ToDateTime(o.Boarding_Time).Date == DateTime.Now.Date)*/.ToList();
            if(TodaySchedules.Count > 0)
            {
                foreach (var n in TodaySchedules)
                {
                    if (DateTime.Now.AddMinutes(-30) >= Convert.ToDateTime(n.Arrival_Time) && (n.Status_ID != 5 || n.Status_ID != 6))
                    {
                        n.Status_ID = 4;
                        db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (DateTime.Now.AddMinutes(-10) >= Convert.ToDateTime(n.Departure_Time) && (n.Status_ID != 5 || n.Status_ID != 6))
                    {
                        n.Status_ID = 3;
                        db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (DateTime.Now >= Convert.ToDateTime(n.Boarding_Time) && (n.Status_ID != 5 || n.Status_ID != 6))
                    {
                        n.Status_ID = 2;
                        db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return null;
        }
    }
}