using CarFuel.Models;
using CarFuel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFuel.Facts.Services {
    public class CarServiceFacts {


        public class Add {

            [Fact]
            public void ValidMake_AddOk() {
                using (var app = new App(testing: true)) {
                    var c = new Car();
                    c.Make = "Honda"; // valid make

                    app.Cars.Add(c);
                    app.Cars.SaveChanges();

                    var n = app.Cars.All().Count();

                    Assert.Equal(1, n);
                }
            }

            [Fact]
            public void InvalidMake_AddFailed() {
                using (var app = new App(testing: true)) {
                    var c = new Car();
                    c.Make = "Google"; // valid make

                    Assert.ThrowsAny<Exception>(() => {
                        app.Cars.Add(c);
                    });
                    

                }
            }

        }

    }
}