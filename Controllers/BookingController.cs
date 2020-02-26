using Buses.Models;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buses.Controllers
{
    public class BookingController : Controller
    {
        private Bus_Ticketing_SystemEntities db = new Bus_Ticketing_SystemEntities();
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BusBooking()
        {
            //ViewBag.Bookings = db.View_Stage_Booking.ToList();
            if (User.IsInRole("Super Administrator"))
            {
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.schedule = db.View_Scheduling.AsNoTracking().ToList();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.Company = db.A_Companies.AsNoTracking().Where(o => o.ID == logginUser.Company_Id).ToList();
                db.Configuration.ProxyCreationEnabled = false;
                ViewBag.schedule = db.View_Scheduling.AsNoTracking().Where(o=>o.Company_ID== logginUser.Company_Id).ToList();
            }
            return View();
        }
        public ActionResult GetBookings(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data = db.View_Stage_Booking_Final.OrderByDescending(O=> O.Boarding_Time).ToList();
            int count = db.View_Stage_Booking_Final.ToList().Count();

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
        public ActionResult AddBooking(string Ticket_No,int Schedule_ID,string CustomerName, string Seat_No)
        {

            if (!string.IsNullOrEmpty(Seat_No))
            {
                if (Seat_No.Contains(","))
                {
                    var a = Seat_No.Split(',');
                    foreach(var b in a)
                    {
                        Stage_Ticketing value = new Stage_Ticketing();
                        value.Ticket_No = Ticket_No;
                        value.Seat_No = Convert.ToInt32(b);
                        value.Schedule_ID = Schedule_ID;
                        value.Ticket_DateTime = DateTime.Now;
                        value.CustomerName = CustomerName;
                        //value.Issued_BY = User.Identity.Name;
                        var check = db.Stage_Ticketing.FirstOrDefault(o => o.Schedule_ID == value.Schedule_ID && o.Seat_No == value.Seat_No);
                        if (check != null)
                        {
                            check.Seat_No = value.Seat_No;
                            check.CustomerName = value.CustomerName;
                            //check.Edited_By = User.Identity.Name;
                            check.Edited_Date = DateTime.Now;
                            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            value.Ticket_No = _ComplaintCode();
                            db.Stage_Ticketing.Add(value);
                        }

                        db.SaveChanges();
                    }
                }
                else
                {
                    Stage_Ticketing value = new Stage_Ticketing();
                    value.Ticket_No = Ticket_No;
                    value.Seat_No = Convert.ToInt32(Seat_No);
                    value.Schedule_ID = Schedule_ID;
                    value.Ticket_DateTime = DateTime.Now;
                    value.CustomerName = CustomerName;
                    //value.Issued_BY = User.Identity.Name;
                    var check = db.Stage_Ticketing.FirstOrDefault(o => o.Schedule_ID == value.Schedule_ID && o.Seat_No == value.Seat_No);
                    if (check != null)
                    {
                        check.Seat_No = value.Seat_No;
                        check.CustomerName = value.CustomerName;
                        //check.Edited_By = User.Identity.Name;
                        check.Edited_Date = DateTime.Now;
                        db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        value.Ticket_No = _ComplaintCode();
                        db.Stage_Ticketing.Add(value);
                    }

                    db.SaveChanges();
                }
            }
            return Content("");
        }
        public string _ComplaintCode()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            var tdate = DateTime.Now;
            string year = tdate.Year.ToString().Trim().Substring(2, 2);
            string month = tdate.Month.ToString();
            string searchCode = "-" + month + "-" + year;
            int count = db.Stage_Ticketing.Where(o => o.Ticket_No.Contains(searchCode)).ToList().Count;
            int code = (count + 1);
            string result = code.ToString("D4") + "-" + month + "-" + year;
            return result;
        }
        public ActionResult GETSelecetedSchedule(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.Stage_Ticketing.AsNoTracking().Where(o => o.Schedule_ID == ID).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GETComplaintCode()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            var tdate = DateTime.Now;
            string year = tdate.Year.ToString().Trim().Substring(2, 2);
            string month = tdate.Month.ToString();
            string searchCode = "-" + month + "-" + year;
            int count = db.Stage_Ticketing.Where(o => o.Ticket_No.Contains(searchCode)).ToList().Count;
            int code = (count + 1);
            string result = code.ToString("D4") + "-" + month + "-" + year;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}