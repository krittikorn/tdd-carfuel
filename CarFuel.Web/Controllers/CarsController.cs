using CarFuel.Models;
using CarFuel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarFuel.Web.Controllers
{
    public class CarsController : Controller
    {

        // private CarService s = new CarService();
        private App app = new App();
        protected override void Dispose(bool disposing) {
            if (disposing) {
                app.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index() {
            var cars = app.Cars.All();
            return View(cars);
        }

        public ActionResult AddCar() {
            var c = new Car();
            c.Make = "Honda";
            c.Model = "Jazz";
            c.Color = "Red";

            var r = new Random();
            var odo = 1000;
            for (int i = 0; i < r.Next(3, 10 + 1); i++) {
                var isForgot = r.Next(100) < 5; // 5% forgot brain level.
                var liters = r.Next(300, 800) / 10.0;

                c.AddFillUp(odo, liters, isFull: true, isForgot: isForgot);

                odo += r.Next(300, 800);
            }

            app.Cars.Add(c);
            app.Cars.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}