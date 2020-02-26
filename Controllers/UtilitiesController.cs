using Buses.Models;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Buses.Controllers
{
    public class UtilitiesController : Controller
    {
        private Bus_Ticketing_SystemEntities db = new Bus_Ticketing_SystemEntities();

        // GET: Utilities
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Companies()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Nationality = db.A_Country.AsNoTracking().ToList();
            return View();
        }

        public ActionResult Routes()
        {
            if (User.IsInRole("Super Administrator"))
            {
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().ToList();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().Where(o=> o.ID== logginUser.Company_Id).ToList();

            }
            return View();
        }

        public ActionResult Buses()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.manufacturer = db.A_Manufacturer.AsNoTracking().ToList();
            if (User.IsInRole("Super Administrator"))
            {
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Route = db.A_Routes.AsNoTracking().ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Employee = db.A_Employees.AsNoTracking().ToList();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().Where(o => o.ID == logginUser.Company_Id).ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Route = db.A_Routes.AsNoTracking().Where(o => o.Company == logginUser.Company_Id).ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Employee = db.A_Employees.AsNoTracking().Where(o => o.Company == logginUser.Company_Id).ToList();
            }
            return View();
        }

        public ActionResult Scheduling()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Status = db.A_Schedule_Status.AsNoTracking().ToList();
            if (User.IsInRole("Super Administrator"))
            {
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Buses = db.A_Buses.AsNoTracking().ToList();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().Where(o => o.ID == logginUser.Company_Id).ToList();

                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Buses = db.A_Buses.AsNoTracking().Where(o => o.Company == logginUser.Company_Id).ToList();
            }
            return View();
        }

        public ActionResult GetCompanies(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data = db.A_Companies.ToList();
            int count = db.A_Companies.ToList().Count();

            DataOperations operation = new DataOperations();
            //Performing filtering operation
            if (dm.Where != null)
            {
                data = operation.PerformWhereFilter(data, dm.Where, "and");
                IEnumerable<object> filtered = (IEnumerable<object>)data;
                count = filtered.Count();
            }
            //Performing search operation
            if (dm.Search != null)
            {
                data = operation.PerformSearching(data, dm.Search);
                IEnumerable<object> searched = (IEnumerable<object>)data;
                count = searched.Count();
            }
            //Performing sorting operation
            if (dm.Sorted != null)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }

            //Performing paging operations
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }

            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }

            return Json(new { result = data, count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanyInsert(A_Companies value)
        {
            A_Companies exists = db.A_Companies.FirstOrDefault(o => o.ID == value.ID);
            if (exists != null)
            {
                exists.Email = value.Email;
                exists.Location = value.Location;
                exists.Name = value.Name;
                exists.CountryCode = value.CountryCode;
                exists.Phone = value.Phone;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Companies data = db.A_Companies.OrderByDescending(o => o.ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.ID;
                }
                value.ID = (count + 1);
                db.A_Companies.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanyUpdate(A_Companies value)
        {
            A_Companies exists = db.A_Companies.FirstOrDefault(o => o.ID == value.ID);
            if (exists != null)
            {
                exists.Email = value.Email;
                exists.Location = value.Location;
                exists.Name = value.Name;
                exists.CountryCode = value.CountryCode;
                exists.Phone = value.Phone;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Companies data = db.A_Companies.OrderByDescending(o => o.ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.ID;
                }

                value.ID = (count + 1);
                db.A_Companies.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoutes(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data;
            int count = 0;
            if (User.IsInRole("Super Administrator"))
            {
                data = db.A_Routes.ToList();
                count = db.A_Routes.ToList().Count();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                data = db.A_Routes.Where(o=> o.Company == logginUser.Company_Id).ToList();
                count = db.A_Routes.Where(o => o.Company == logginUser.Company_Id).ToList().Count();

            }

            DataOperations operation = new DataOperations();
            //Performing filtering operation
            if (dm.Where != null)
            {
                data = operation.PerformWhereFilter(data, dm.Where, "and");
                IEnumerable<object> filtered = (IEnumerable<object>)data;
                count = filtered.Count();
            }
            //Performing search operation
            if (dm.Search != null)
            {
                data = operation.PerformSearching(data, dm.Search);
                IEnumerable<object> searched = (IEnumerable<object>)data;
                count = searched.Count();
            }
            //Performing sorting operation
            if (dm.Sorted != null)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }

            //Performing paging operations
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }

            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }

            return Json(new { result = data, count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoutesInsert(A_Routes value)
        {
            A_Routes exists = db.A_Routes.FirstOrDefault(o => o.Route_ID == value.Route_ID);
            if (exists != null)
            {
                exists.Route_Name = value.Route_Name;
                exists.Start_Stage = value.Start_Stage;
                exists.Destination_Stage = value.Destination_Stage;
                exists.Company = value.Company;
                exists.Route_Description = value.Route_Description;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Routes data = db.A_Routes.OrderByDescending(o => o.Route_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Route_ID;
                }
                value.Route_ID = (count + 1);
                db.A_Routes.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoutesUpdate(A_Routes value)
        {
            A_Routes exists = db.A_Routes.FirstOrDefault(o => o.Route_ID == value.Route_ID);
            if (exists != null)
            {
                exists.Route_Name = value.Route_Name;
                exists.Start_Stage = value.Start_Stage;
                exists.Destination_Stage = value.Destination_Stage;
                exists.Company = value.Company;
                exists.Route_Description = value.Route_Description;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Routes data = db.A_Routes.OrderByDescending(o => o.Route_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Route_ID;
                }
                value.Route_ID = (count + 1);
                db.A_Routes.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBuses(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data;
            int count = 0;
            if (User.IsInRole("Super Administrator"))
            {
                data = db.A_Buses.ToList();
                count = db.A_Buses.ToList().Count();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                data = db.A_Buses.Where(o=> o.Company== logginUser.Company_Id).ToList();
                count = db.A_Buses.Where(o => o.Company == logginUser.Company_Id).ToList().Count();

            }

            DataOperations operation = new DataOperations();
            //Performing filtering operation
            if (dm.Where != null)
            {
                data = operation.PerformWhereFilter(data, dm.Where, "and");
                IEnumerable<object> filtered = (IEnumerable<object>)data;
                count = filtered.Count();
            }
            //Performing search operation
            if (dm.Search != null)
            {
                data = operation.PerformSearching(data, dm.Search);
                IEnumerable<object> searched = (IEnumerable<object>)data;
                count = searched.Count();
            }
            //Performing sorting operation
            if (dm.Sorted != null)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }

            //Performing paging operations
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }

            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }

            return Json(new { result = data, count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusesInsert(A_Buses value)
        {
            A_Buses exists = db.A_Buses.FirstOrDefault(o => o.Bus_ID == value.Bus_ID);
            if (exists != null)
            {
                exists.Manufacturer = value.Manufacturer;
                exists.Model = value.Model;
                exists.Plate_No = value.Plate_No;
                exists.Company = value.Company;
                exists.Route_ID = value.Route_ID;
                exists.Seat_No = value.Seat_No;
                exists.Driver_ID = value.Driver_ID;
                exists.Company = value.Company;
                //exists.CreatedBy = value.CreatedBy;
                //exists.CreationDate = value.CreationDate;
                exists.EditedBy = User.Identity.Name;
                exists.EditionDate = DateTime.Now;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Buses data = db.A_Buses.OrderByDescending(o => o.Bus_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Bus_ID;
                }
                value.Bus_ID = (count + 1);
                value.CreatedBy = User.Identity.Name;
                value.CreationDate = DateTime.Now;
                db.A_Buses.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusesUpdate(A_Buses value)
        {
            A_Buses exists = db.A_Buses.FirstOrDefault(o => o.Bus_ID == value.Bus_ID);
            if (exists != null)
            {
                exists.Manufacturer = value.Manufacturer;
                exists.Model = value.Model;
                exists.Plate_No = value.Plate_No;
                exists.Company = value.Company;
                exists.Route_ID = value.Route_ID;
                exists.Seat_No = value.Seat_No;
                exists.Driver_ID = value.Driver_ID;
                exists.Company = value.Company;
                //exists.CreatedBy = value.CreatedBy;
                //exists.CreationDate = value.CreationDate;
                exists.EditedBy = User.Identity.Name;
                exists.EditionDate = DateTime.Now;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Buses data = db.A_Buses.OrderByDescending(o => o.Bus_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Bus_ID;
                }
                value.Bus_ID = (count + 1);
                value.CreatedBy = User.Identity.Name;
                value.CreationDate = DateTime.Now;
                db.A_Buses.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSchedules(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data;
            int count = 0;
            if (User.IsInRole("Super Administrator"))
            {
                data = db.Bus_Scheduling.ToList();
                count = db.Bus_Scheduling.ToList().Count();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                data = db.Bus_Scheduling.Where(o => o.Company_ID == logginUser.Company_Id).ToList();
                count = db.Bus_Scheduling.Where(o => o.Company_ID == logginUser.Company_Id).ToList().Count();

            }
            DataOperations operation = new DataOperations();
            //Performing filtering operation
            if (dm.Where != null)
            {
                data = operation.PerformWhereFilter(data, dm.Where, "and");
                IEnumerable<object> filtered = (IEnumerable<object>)data;
                count = filtered.Count();
            }
            //Performing search operation
            if (dm.Search != null)
            {
                data = operation.PerformSearching(data, dm.Search);
                IEnumerable<object> searched = (IEnumerable<object>)data;
                count = searched.Count();
            }
            //Performing sorting operation
            if (dm.Sorted != null)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }

            //Performing paging operations
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }

            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }

            return Json(new { result = data, count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchedulesInsert(Bus_Scheduling value)
        {
            Bus_Scheduling exists = db.Bus_Scheduling.FirstOrDefault(o => o.ID == value.ID);
            if (exists != null)
            {
                exists.Status_ID = value.Status_ID;
                exists.Arrival_Time = value.Arrival_Time;
                exists.Bus_ID = value.Bus_ID;
                exists.Company_ID = value.Company_ID;
                exists.Departure_Time = value.Departure_Time;
                exists.Boarding_Time = value.Boarding_Time;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                Bus_Scheduling data = db.Bus_Scheduling.OrderByDescending(o => o.ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.ID;
                }
                value.ID = (count + 1);
                db.Bus_Scheduling.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchedulesUpdate(Bus_Scheduling value)
        {
            Bus_Scheduling exists = db.Bus_Scheduling.FirstOrDefault(o => o.ID == value.ID);
            if (exists != null)
            {
                exists.Status_ID = value.Status_ID;
                exists.Arrival_Time = value.Arrival_Time;
                exists.Bus_ID = value.Bus_ID;
                exists.Company_ID = value.Company_ID;
                exists.Departure_Time = value.Departure_Time;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                Bus_Scheduling data = db.Bus_Scheduling.OrderByDescending(o => o.ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.ID;
                }
                value.ID = (count + 1);
                db.Bus_Scheduling.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}