using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessTrips.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Calabonga.Xml.Exports;
using System.Globalization;
using System.Text;
using System.Threading;

namespace BusinessTrips.Controllers
{
    [Authorize(Users = "admin@mailSibCemAdmin.ru, kadr@mailSibCemKadr.ru")]
    public class EmployeesController : Controller
    {
        private BusinessTripsContext db = new BusinessTripsContext();

        [HttpPost]
        public ActionResult Search(string surname)
         {
            if (surname == "")
            {
                ViewBag.Message = "Некорректные данные для поиска!";
                return PartialView("Search");
            }

            var allEmpls = db.Employees.Where(a => a.Surname.Contains(surname)).ToList();

            if (allEmpls.Count <= 0)
            {
                ViewBag.Message = "Сотрудник с такой фамилией не найден!";
                return PartialView("Search");
            }

            List<Employee> Employees = db.Employees.ToList();
            List<Hotel> Hotels = db.Hotels.ToList();
            List<Passage> Passages = db.Passeges.ToList();

                          
            var dutyJourneyListAll = from employee in Employees
                                     join hotel in Hotels on employee.DutyJourneyId equals hotel.Id
                                     join passage in Passages on hotel.DutyJourneyId equals passage.Id
                                     select new Employee
                                     {
                                         Name = employee.Name,
                                         City = hotel.City,
                                         Transport = passage.Transport
                                     };


            List<Employee> empls = dutyJourneyListAll.ToList();
            return PartialView("Search", empls);

        }

        public ActionResult Export()
        {
            string result = string.Empty;
            Workbook wb = new Workbook();

            // properties
            wb.Properties.Author = "Calabonga";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "Calabonga";
            wb.Properties.Version = "14";

            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;

            // get data
            var employeeCompositions = db.Employees.ToList();
            Worksheet em3 = new Worksheet("Сотрудники");
            em3.AddCell(0, 0, "Имя");
            em3.AddCell(0, 1, "Фамилия");
            em3.AddCell(0, 2, "Отчество");
            em3.AddCell(0, 3, "Дата рождения");
            em3.AddCell(0, 4, "Должность");
            em3.AddCell(0, 5, "Номер паспорта");
            em3.AddCell(0, 6, "Номер телефона");
            int totalRows = 0;

            // appending rows with data
            for (int i = 0; i < employeeCompositions.Count; i++)
            {
                em3.AddCell(i + 1, 0, employeeCompositions[i].Name);
                em3.AddCell(i + 1, 1, employeeCompositions[i].Surname);
                em3.AddCell(i + 1, 2, employeeCompositions[i].Lastname);
                em3.AddCell(i + 1, 3, employeeCompositions[i].BirthDate.ToString());
                em3.AddCell(i + 1, 4, employeeCompositions[i].OfficialPosition);
                em3.AddCell(i + 1, 5, employeeCompositions[i].Pasport);
                em3.AddCell(i + 1, 6, employeeCompositions[i].NumberPhone);

                totalRows++;
            }

            wb.AddWorksheet(em3);

            // generate xml 
            string workstring = wb.ExportToXML();

            // Send to user file
            return new ExcelResult("Employee.xls", workstring);
        }
        public class ExcelResult : ActionResult
        {
            /// <summary>
            /// Создает экземпляр класса, которые выдает файл Excel
            /// </summary>
            /// <param name="fileName">наименование файла для экспорта</param>
            /// <param name="report">готовый набор данные для экпорта</param>
            public ExcelResult(string fileName, string report)
            {
                this.Filename = fileName;
                this.Report = report;
            }
            public string Report { get; private set; }
            public string Filename { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var ctx1 = context.HttpContext;
                ctx1.Response.Clear();
                ctx1.Response.ContentType = "application/vnd.ms-excel";
                ctx1.Response.BufferOutput = true;
                ctx1.Response.AddHeader("content-disposition",
                    string.Format("attachment; filename={0}", Filename));
                ctx1.Response.ContentEncoding = Encoding.UTF8;
                ctx1.Response.Charset = "utf-8";
                ctx1.Response.Write(Report);
                ctx1.Response.Flush();
                ctx1.Response.End();
            }
        }

        [HttpPost]
        [Authorize(Users = "admin@mailSibCemAdmin.ru")]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Файл не выбран! <br>";
                return View("Index", db.Employees.ToList());
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    //string path = System.Web.Hosting.HostingEnvironment.MapPath(excelfile.FileName);
                    //string path = @"C:\Users\Михаил\Source\Repos\BusinessTrips\BusinessTrips\Import\" + excelfile.FileName;
                    string path = Server.MapPath("../Import/") + excelfile.FileName.Split('\\').Last();
                    excelfile.SaveAs(path);
                    //Читаем из файла
                    // Excel.Application ap = new Excel.Application();


                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<Employee> employeeCompositions = new List<Employee>();
                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        Employee employeeComposition = new Employee();
                        employeeComposition.Name = Convert.ToString(((Excel.Range)range.Cells[row, 1]).Text);
                        employeeComposition.Surname = Convert.ToString(((Excel.Range)range.Cells[row, 2]).Text);
                        employeeComposition.Lastname = Convert.ToString(((Excel.Range)range.Cells[row, 3]).Text);
                        employeeComposition.BirthDate = Convert.ToDateTime(((Excel.Range)range.Cells[row, 4]).Text);
                        employeeComposition.OfficialPosition = Convert.ToString(((Excel.Range)range.Cells[row, 5]).Text);
                        employeeComposition.Pasport = Convert.ToString(((Excel.Range)range.Cells[row, 6]).Text);
                        employeeComposition.NumberPhone = Convert.ToString(((Excel.Range)range.Cells[row, 7]).Text);
                        db.Employees.Add(employeeComposition);
                        db.SaveChanges();
                    }
                    workbook.Close();
                    ViewBag.Error = "Данные загружены <br>";
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    return View("Index", db.Employees.ToList());
                }
                else
                {
                    ViewBag.Error = "Это не Excel! <br>";
                    return View("Index", db.Employees.ToList());
                }
            }

        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.DutyJourneys);
            return View(employees.ToList());
        }
        [Authorize(Users = "kadr@mailSibCemKadr.ru")]
        public ActionResult Request()
        {
            var employees = db.Employees.Include(e => e.DutyJourneys);
            return View(db.Employees.ToList());
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "Point");
            return View();
        }

        // POST: Employees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Lastname,BirthDate,OfficialPosition,Pasport,NumberPhone,DutyJourneyId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "Point", employee.DutyJourneyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "Point", employee.DutyJourneyId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Lastname,BirthDate,OfficialPosition,Pasport,NumberPhone,DutyJourneyId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "Point", employee.DutyJourneyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Users = "admin@mailSibCemAdmin.ru")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@mailSibCemAdmin.ru")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
