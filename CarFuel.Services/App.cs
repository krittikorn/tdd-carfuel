using CarFuel.DataAccess.Context;
using CarFuel.DataAccess.Repositories;
using CarFuel.Models;
using GFX.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services {
    public class App : RootClass {

        public bool TestingMode { get; private set; }

        public App(bool testing = false)
            : base(testing) {

            this.TestingMode = testing;
        }


        protected override DbContext NewDbContext() {
            return new CarDb();
        }

        protected override void RegisterServices() {
            this.AddService<Car, CarService, CarRepository>();
        }

        protected override void RegisterServicesForUnitTests() {
            this.AddService<Car, CarService, FakeRepository<Car>>();
        }


        #region Services
        public CarService Cars {
            get {
                return this.Services<Car, CarService>();
            }
        }
        #endregion

    }
}