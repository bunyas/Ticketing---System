using Buses.Models;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buses.Controllers
{
    public class EmployeesController : Controller
    {
        private Bus_Ticketing_SystemEntities db = new Bus_Ticketing_SystemEntities();

        // GET: Employees
        public ActionResult Index()
        {
            // var employee = db.A_Employees.ToList();
            //ViewBag.Employee = employee;
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Company = db.A_Companies.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Gender = db.A_Gender.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Nationality = db.A_Country.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Titles = db.A_Personel_Titles.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Education_Level = db.A_Level_Of_Education.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Contact_Type = db.A_Contact_Type.AsNoTracking().ToList();
            return View();
        }
        public ActionResult Employees()
        {
            // var employee = db.A_Employees.ToList();
            //ViewBag.Employee = employee;
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Gender = db.A_Gender.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Nationality = db.A_Country.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Titles = db.A_Personel_Titles.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Education_Level = db.A_Level_Of_Education.AsNoTracking().ToList();
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Contact_Type = db.A_Contact_Type.AsNoTracking().ToList();
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
                ViewBag.Company = db.A_Companies.AsNoTracking().Where(o => o.ID == logginUser.Company_Id).ToList();

            }
            return View();
        }
        public ActionResult GetEmployes(Syncfusion.JavaScript.DataManager dm/*, string compCode*/)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data;
            int count = 0;
            if (User.IsInRole("Super Administrator"))
            {
                data = db.A_Employees.Select(o => new { o.Company, o.CreatedBy, o.CreationDate, o.Date_Of_Birth, o.EditedBy, o.EditionDate, o.Email, o.Employee_ID, o.Gender, o.Name, o.Nationality, o.NIN_No, o.Phone, o.Telephone, o.Title, o.User_Id }).ToList();
                count = db.A_Employees.ToList().Count();
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var logginUser = db.View_Users.FirstOrDefault(o => o.UserId == userid);
                data = db.A_Employees.Where(o=> o.Company== logginUser.Company_Id).Select(o => new { o.Company, o.CreatedBy, o.CreationDate, o.Date_Of_Birth, o.EditedBy, o.EditionDate, o.Email, o.Employee_ID, o.Gender, o.Name, o.Nationality, o.NIN_No, o.Phone, o.Telephone, o.Title, o.User_Id }).ToList();
                count = db.A_Employees.Where(o => o.Company == logginUser.Company_Id).ToList().Count();

            }
            //var muserrole = User.IsInRole("HSIPClient");
            //if (muserrole)
            //{
            //    int? facility_id = new UserManagement().getCurrentuserFacility();
            //    List<fo_complaint> m = new List<fo_complaint>();
            //    var _data = db.View_fo_complaint_AffectedSites.Where(e => e.FacilityCode == facility_id.ToString() && e.FinalSubmission != 1 && e.DeletedRecord != true && e.e_reg_complaint_status != 3).OrderByDescending(o => o.e_reg_date_recieved).ToList();
            //    count = db.View_fo_complaint_AffectedSites.Where(e => e.FacilityCode == facility_id.ToString() && e.FinalSubmission != 1 && e.DeletedRecord != true && e.e_reg_complaint_status != 3).ToList().Count();
            //    foreach (var n in _data)
            //    {
            //        fo_complaint x = new fo_complaint();
            //        x.AffectedSites = n.AffectedSites;
            //        x.Brief_Feedback_Desc = n.Brief_Feedback_Desc;
            //        x.Communicated_By_Lev1 = n.Communicated_By_Lev1;
            //        x.Communicated_By_Lev1Date = n.Communicated_By_Lev1Date;
            //        x.Communicated_By_Lev1Title = n.Communicated_By_Lev1Title;
            //        x.Communicated_By_Lev2 = n.Communicated_By_Lev2;
            //        x.Communicated_By_Lev2Date = n.Communicated_By_Lev2Date;
            //        x.Communicated_By_Lev2Email = n.Communicated_By_Lev2Email;
            //        x.Communicated_By_Lev2Title = n.Communicated_By_Lev2Title;
            //        x.ComplainantDistrict = n.ComplainantDistrict;
            //        x.ComplainantEmail = n.ComplainantEmail;
            //        x.ComplainantFacilityCode = n.ComplainantFacilityCode;
            //        x.ComplainantMobile = n.ComplainantMobile;
            //        x.ComplainantName = n.ComplainantName;
            //        x.ComplainantPhone = n.ComplainantPhone;
            //        x.ComplainantTitle = n.ComplainantTitle;
            //        x.DeletedRecord = n.DeletedRecord;
            //        x.Email_letter_attached = n.Email_letter_attached;
            //        x.e_reg_affected_sites = n.e_reg_affected_sites;
            //        x.e_reg_communication_mode = n.e_reg_communication_mode;
            //        x.e_reg_complaint_accuteness = n.e_reg_complaint_accuteness;
            //        x.e_reg_complaint_category = n.e_reg_complaint_category;
            //        x.e_reg_complaint_code = n.e_reg_complaint_code;
            //        x.e_reg_complaint_details = n.e_reg_complaint_details;
            //        x.e_reg_complaint_No = n.e_reg_complaint_No;
            //        x.e_reg_complaint_status = n.e_reg_complaint_status;
            //        x.e_reg_complaint_Title = n.e_reg_complaint_Title;
            //        x.e_reg_contact_person_id = n.e_reg_contact_person_id;
            //        x.e_reg_date_complaint = n.e_reg_date_complaint;
            //        x.e_reg_date_recieved = n.e_reg_date_recieved;
            //        x.e_reg_date_resolved = n.e_reg_date_resolved;
            //        x.e_reg_expected_date_resolution = n.e_reg_expected_date_resolution;
            //        x.e_reg_MAUL_Staff = n.e_reg_MAUL_Staff;
            //        x.e_reg_product_category = n.e_reg_product_category;
            //        x.Feedback = n.Feedback;
            //        x.Feedback_Communicated = n.Feedback_Communicated;
            //        x.Feedback_Date = n.Feedback_Date;
            //        x.IP = n.IP;
            //        x.is_quality_issue = n.is_quality_issue;
            //        x.No_Feedback_Reason = n.No_Feedback_Reason;
            //        x.Prod_Samples_Provided = n.Prod_Samples_Provided;
            //        x.Sup_Doc_Evidence_Rec = n.Sup_Doc_Evidence_Rec;
            //        x.FinalSubmission = n.FinalSubmission;
            //        x.ComplainantCategory = n.ComplainantCategory;
            //        m.Add(x);
            //    }
            //    data = m;
            //}
            //else if (User.IsInRole("SCTO"))
            //{
            //    var Username = User.Identity.Name;
            //    var person = db.AspNetUsers.FirstOrDefault(o => o.UserName == Username);
            //    var contact = db.fo_contact_person.FirstOrDefault(o => o.cp_name.Trim() == person.Name.Trim());
            //    var RFSOfacilities = new UserManagement().getUserFacilityList();
            //    var _data = db.View_fo_complaint_AffectedSites.Where(e => (RFSOfacilities.Any(p => e.FacilityCode.Trim() == p.ToString().Trim())) && e.FinalSubmission == 1 && e.DeletedRecord != true && e.e_reg_complaint_status == 1 && (e.Level1_Assignment.Contains(contact.cp_code.ToString()) || e.Level2_Assignment.Contains(contact.cp_code.ToString()))).OrderByDescending(o => o.e_reg_date_recieved).ToList();
            //    count = db.View_fo_complaint_AffectedSites.Where(e => (RFSOfacilities.Any(p => e.FacilityCode.Trim() == p.ToString().Trim())) && e.FinalSubmission == 1 && e.DeletedRecord != true && e.e_reg_complaint_status == 1 && (e.Level1_Assignment.Contains(contact.cp_code.ToString()) || e.Level2_Assignment.Contains(contact.cp_code.ToString()))).ToList().Count();
            //    List<fo_complaint> m = new List<fo_complaint>();
            //    foreach (var n in _data)
            //    {
            //        fo_complaint x = new fo_complaint();
            //        x.AffectedSites = n.AffectedSites;
            //        x.Brief_Feedback_Desc = n.Brief_Feedback_Desc;
            //        x.Communicated_By_Lev1 = n.Communicated_By_Lev1;
            //        x.Communicated_By_Lev1Date = n.Communicated_By_Lev1Date;
            //        x.Communicated_By_Lev1Title = n.Communicated_By_Lev1Title;
            //        x.Communicated_By_Lev2 = n.Communicated_By_Lev2;
            //        x.Communicated_By_Lev2Date = n.Communicated_By_Lev2Date;
            //        x.Communicated_By_Lev2Email = n.Communicated_By_Lev2Email;
            //        x.Communicated_By_Lev2Title = n.Communicated_By_Lev2Title;
            //        x.ComplainantDistrict = n.ComplainantDistrict;
            //        x.ComplainantEmail = n.ComplainantEmail;
            //        x.ComplainantFacilityCode = n.ComplainantFacilityCode;
            //        x.ComplainantMobile = n.ComplainantMobile;
            //        x.ComplainantName = n.ComplainantName;
            //        x.ComplainantPhone = n.ComplainantPhone;
            //        x.ComplainantTitle = n.ComplainantTitle;
            //        x.DeletedRecord = n.DeletedRecord;
            //        x.Email_letter_attached = n.Email_letter_attached;
            //        x.e_reg_affected_sites = n.e_reg_affected_sites;
            //        x.e_reg_communication_mode = n.e_reg_communication_mode;
            //        x.e_reg_complaint_accuteness = n.e_reg_complaint_accuteness;
            //        x.e_reg_complaint_category = n.e_reg_complaint_category;
            //        x.e_reg_complaint_code = n.e_reg_complaint_code;
            //        x.e_reg_complaint_details = n.e_reg_complaint_details;
            //        x.e_reg_complaint_No = n.e_reg_complaint_No;
            //        x.e_reg_complaint_status = n.e_reg_complaint_status;
            //        x.e_reg_complaint_Title = n.e_reg_complaint_Title;
            //        x.e_reg_contact_person_id = n.e_reg_contact_person_id;
            //        x.e_reg_date_complaint = n.e_reg_date_complaint;
            //        x.e_reg_date_recieved = n.e_reg_date_recieved;
            //        x.e_reg_date_resolved = n.e_reg_date_resolved;
            //        x.e_reg_expected_date_resolution = n.e_reg_expected_date_resolution;
            //        x.e_reg_MAUL_Staff = n.e_reg_MAUL_Staff;
            //        x.e_reg_product_category = n.e_reg_product_category;
            //        x.Feedback = n.Feedback;
            //        x.Feedback_Communicated = n.Feedback_Communicated;
            //        x.Feedback_Date = n.Feedback_Date;
            //        x.IP = n.IP;
            //        x.is_quality_issue = n.is_quality_issue;
            //        x.No_Feedback_Reason = n.No_Feedback_Reason;
            //        x.Prod_Samples_Provided = n.Prod_Samples_Provided;
            //        x.Sup_Doc_Evidence_Rec = n.Sup_Doc_Evidence_Rec;
            //        x.FinalSubmission = n.FinalSubmission;
            //        x.ComplainantCategory = n.ComplainantCategory;
            //        m.Add(x);
            //    }
            //    data = m;
            //}
            //else if (User.IsInRole("ComplaintHandling") || User.IsInRole("MonitoringAndEvaluationOfficer"))
            //{
            //    var Username = User.Identity.Name;
            //    var person = db.AspNetUsers.FirstOrDefault(o => o.UserName == Username);
            //    var contact = db.fo_contact_person.FirstOrDefault(o => o.cp_name.Trim() == person.Name.Trim());
            //    data = db.fo_complaint.Where(o => o.FinalSubmission == 1 && o.DeletedRecord != true && o.e_reg_complaint_status == 1 && (o.Level1_Assignment.Contains(contact.cp_code.ToString()) || o.Level2_Assignment.Contains(contact.cp_code.ToString()))).OrderByDescending(o => o.e_reg_date_recieved).ToList();
            //    count = db.fo_complaint.Where(o => o.FinalSubmission == 1 && o.DeletedRecord != true && o.e_reg_complaint_status == 1 && (o.Level1_Assignment.Contains(contact.cp_code.ToString()) || o.Level2_Assignment.Contains(contact.cp_code.ToString()))).ToList().Count();
            //}
            //else
            //{
            //    data = db.fo_complaint.Where(o => o.FinalSubmission == 1 && o.DeletedRecord != true && o.e_reg_complaint_status != 3).OrderByDescending(o => o.e_reg_date_recieved).ToList();
            //    count = db.fo_complaint.Where(o => o.FinalSubmission == 1 && o.DeletedRecord != true && o.e_reg_complaint_status != 3).ToList().Count();
            //}

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

        public ActionResult GetEducation(Syncfusion.JavaScript.DataManager dm, int? EmployeeID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data = db.View_Employee_Education.Where(o => o.EmployeeID == EmployeeID).ToList();
            int count = db.View_Employee_Education.Where(o => o.EmployeeID == EmployeeID).ToList().Count();

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

        public ActionResult GetNOK(Syncfusion.JavaScript.DataManager dm, int? EmployeeID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable data = db.Employee_NOK.Where(o => o.Employee_ID == EmployeeID).ToList();
            int count = db.Employee_NOK.Where(o => o.Employee_ID == EmployeeID).ToList().Count();

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

        public ActionResult SavePhoto(IEnumerable<HttpPostedFileBase> uploadbox, int? EmployeeID)
        {
            if (uploadbox != null)
            {
                foreach (HttpPostedFileBase file in uploadbox)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string destinationPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(destinationPath);
                    string filePath = destinationPath; // getting the file path of uploaded file
                    string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file
                    string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file
                    string type = string.Empty;
                    Stream fs = file.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files
                    byte[] bytes = br.ReadBytes((int)fs.Length); //counting the file length into bytes
                    A_Employees exists = db.A_Employees.FirstOrDefault(o => o.Employee_ID == EmployeeID);
                    if (exists != null)
                    {
                        exists.Photo = bytes;
                        db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();

                    //int count = context.A_DocumentUpload.ToList().Count;
                    //A_DocumentUpload doc = new A_DocumentUpload()
                    //{
                    //    MeetingNo = meetingNo,
                    //    DecisionType = decisionType,
                    //    DocumentFile = bytes,
                    //    UploadDate = DateTime.Now,
                    //    DocumentName = filename1,
                    //    DocumentCode = (count + 1),
                    //    File_extension = ext
                    //};
                    //context.A_DocumentUpload.Add(doc);
                    //context.SaveChanges();
                }
            }

            return Content("");
        }

        public ActionResult SaveEducDoc(IEnumerable<HttpPostedFileBase> EducDocs, int EmployeeID, int educ_Level)
        {
            if (EducDocs != null)
            {
                foreach (HttpPostedFileBase file in EducDocs)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string destinationPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(destinationPath);
                    string filePath = destinationPath; // getting the file path of uploaded file
                    string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file
                    string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file
                    string type = string.Empty;
                    Stream fs = file.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files
                    byte[] bytes = br.ReadBytes((int)fs.Length); //counting the file length into bytes
                    Education exists = db.Educations.FirstOrDefault(o => o.EmployeeID == EmployeeID && o.Level_Of_Education == educ_Level);
                    if (exists != null)
                    {
                        exists.Certificate = bytes;
                        db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    //else
                    //{
                    //    var count = db.Educations.ToList().Count;

                    //}
                    //int count = context.A_DocumentUpload.ToList().Count;
                    //A_DocumentUpload doc = new A_DocumentUpload()
                    //{
                    //    MeetingNo = meetingNo,
                    //    DecisionType = decisionType,
                    //    DocumentFile = bytes,
                    //    UploadDate = DateTime.Now,
                    //    DocumentName = filename1,
                    //    DocumentCode = (count + 1),
                    //    File_extension = ext
                    //};
                    //context.A_DocumentUpload.Add(doc);
                    //context.SaveChanges();
                }
            }
            return Content("");
        }

        public ActionResult GetEmployeeID()
        {
            int data = db.A_Employees.ToList().Count;
            A_Employees employee = new A_Employees()
            {
                Employee_ID = (data + 1)
            };
            db.A_Employees.Add(employee);
            db.SaveChanges();
            data += 1;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelEmployeeAdd(int employeeID)
        {
            string result = "There was nothing to delete";
            A_Employees data = db.A_Employees.FirstOrDefault(o => o.Employee_ID == employeeID);
            if (data != null)
            {
                db.A_Employees.Remove(data);
                List<Education> educ = db.Educations.Where(o => o.EmployeeID == employeeID).ToList();
                if (educ.Count > 0)
                {
                    db.Educations.RemoveRange(educ);
                }
                List<Employee_NOK> nok = db.Employee_NOK.Where(o => o.Employee_ID == employeeID).ToList();
                if (nok.Count > 0)
                {
                    db.Employee_NOK.RemoveRange(nok);
                }
                db.SaveChanges();
                result = "Deleted successfully";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeInsert(A_Employees value)
        {
            A_Employees exists = db.A_Employees.FirstOrDefault(o => o.Employee_ID == value.Employee_ID);
            if (exists != null)
            {
                exists.Company = value.Company;
                exists.CreatedBy = User.Identity.Name;
                exists.CreationDate = DateTime.Now.Date;
                exists.Date_Of_Birth = value.Date_Of_Birth;
                exists.Email = value.Email;
                exists.Gender = value.Gender;
                exists.Name = value.Name;
                exists.Nationality = value.Nationality;
                exists.NIN_No = value.NIN_No;
                exists.Phone = value.Phone;
                exists.Telephone = value.Telephone;
                exists.Title = value.Title;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Employees data = db.A_Employees.OrderByDescending(o => o.Employee_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Employee_ID;
                }
                value.Employee_ID = (count + 1);
                value.CreatedBy = User.Identity.Name;
                value.CreationDate = DateTime.Now.Date;
                db.A_Employees.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeUpdate(A_Employees value)
        {
            A_Employees exists = db.A_Employees.FirstOrDefault(o => o.Employee_ID == value.Employee_ID);
            if (exists != null)
            {
                exists.EditedBy = User.Identity.Name;
                exists.EditionDate = DateTime.Now.Date;
                exists.Company = value.Company;
                exists.CreatedBy = value.CreatedBy;
                exists.CreationDate = value.CreationDate;
                exists.Date_Of_Birth = value.Date_Of_Birth;
                exists.Email = value.Email;
                exists.Gender = value.Gender;
                exists.Name = value.Name;
                exists.Nationality = value.Nationality;
                exists.NIN_No = value.NIN_No;
                exists.Phone = value.Phone;
                exists.Telephone = value.Telephone;
                exists.Title = value.Title;
                db.Entry(exists).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                int count = 0;
                A_Employees data = db.A_Employees.OrderByDescending(o => o.Employee_ID).ToList().FirstOrDefault();
                if (data != null)
                {
                    count = data.Employee_ID;
                }

                value.Employee_ID = (count + 1);
                value.CreatedBy = User.Identity.Name;
                value.CreationDate = DateTime.Now.Date;
                db.A_Employees.Add(value);
            }
            db.SaveChanges();
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BatchEducationUpdate(string key, List<View_Employee_Education> changed, List<View_Employee_Education> added, List<View_Employee_Education> deleted)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int recordssaved = 0;
            //Performing insert operation
            if (added != null && added.Count() > 0)
            {
                foreach (View_Employee_Education temp in added)
                {
                    Education old = db.Educations.FirstOrDefault(o => o.Level_Of_Education == temp.Level_Of_Education && o.EmployeeID == temp.EmployeeID);

                    if (old != null)
                    {
                        old.Grade = temp.Grade;
                        old.Level_Of_Education = temp.Level_Of_Education;
                        old.Name = temp.Name;
                        old.School = temp.School;
                        old.Start_Year = temp.Start_Year;
                        old.End_Year = temp.End_Year;
                        db.Entry(old).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        int count = db.Educations.ToList().Count;
                        temp.Id = (count + 1);
                        Education value = new Education
                        {
                            Id = temp.Id,
                            Grade = temp.Grade,
                            Level_Of_Education = temp.Level_Of_Education,
                            Name = temp.Name,
                            EmployeeID = temp.EmployeeID,
                            School = temp.School,
                            Start_Year = temp.Start_Year,
                            End_Year = temp.End_Year
                        };
                        db.Educations.Add(value);
                    }
                }
                recordssaved = db.SaveChanges();
            }

            ////Performing update operation
            if (changed != null && changed.Count() > 0)
            {
                foreach (View_Employee_Education temp in changed)
                {
                    Education old = db.Educations.FirstOrDefault(o => o.Id == temp.Id && o.EmployeeID == temp.EmployeeID);

                    if (old != null)
                    {
                        old.Grade = temp.Grade;
                        old.Level_Of_Education = temp.Level_Of_Education;
                        old.Name = temp.Name;
                        old.School = temp.School;
                        old.Start_Year = temp.Start_Year;
                        old.End_Year = temp.End_Year;
                        db.Entry(old).State = System.Data.Entity.EntityState.Modified;
                    }
                    //else
                    //{
                    //    db.fo_complaint_investigation.Add(temp);
                    //}
                }
                recordssaved += db.SaveChanges();
            }

            //Performing delete operation
            if (deleted != null && deleted.Count() > 0)
            {
                foreach (View_Employee_Education temp in deleted)
                {
                    db.Educations.Remove(db.Educations.FirstOrDefault(o => o.Id == temp.Id));
                }
                recordssaved += db.SaveChanges();
            }

            return (null);
        }

        public ActionResult BatchEmployee_NOKUpdate(string key, List<Employee_NOK> changed, List<Employee_NOK> added, List<Employee_NOK> deleted)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int recordssaved = 0;
            //Performing insert operation
            if (added != null && added.Count() > 0)
            {
                foreach (Employee_NOK temp in added)
                {
                    Employee_NOK old = db.Employee_NOK.FirstOrDefault(o => o.ID == temp.ID && o.Employee_ID == temp.Employee_ID);

                    if (old != null)
                    {
                        db.Entry(old).CurrentValues.SetValues(temp);
                    }
                    else
                    {
                        int count = db.Employee_NOK.ToList().Count;
                        temp.ID = (count + 1);
                        db.Employee_NOK.Add(temp);
                    }
                }
                recordssaved = db.SaveChanges();
            }

            ////Performing update operation
            if (changed != null && changed.Count() > 0)
            {
                foreach (Employee_NOK temp in changed)
                {
                    Employee_NOK old = db.Employee_NOK.FirstOrDefault(o => o.ID == temp.ID && o.Employee_ID == temp.Employee_ID);

                    if (old != null)
                    {
                        db.Entry(old).CurrentValues.SetValues(temp);
                    }
                    //else
                    //{
                    //    db.fo_complaint_investigation.Add(temp);
                    //}
                }
                recordssaved += db.SaveChanges();
            }

            //Performing delete operation
            if (deleted != null && deleted.Count() > 0)
            {
                foreach (Employee_NOK temp in deleted)
                {
                    db.Employee_NOK.Remove(db.Employee_NOK.FirstOrDefault(o => o.ID == temp.ID));
                }
                recordssaved += db.SaveChanges();
            }

            return (null);
        }
    }
}