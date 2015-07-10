using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using CarFuel.Models;

namespace CarFuel.Facts {

    public class CarFacts {

        public class GeneralUsage {

            [Fact]
            public void NewCar_HasCorrectInitialValues() {
                Car c = new Car();

                c.Make = "Honda";
                c.Model = "Accord";

                Assert.Equal("Honda", c.Make);
                Assert.Equal("Accord", c.Model);
                Assert.Equal(0, c.FillUps.Count());
            }
        }

        public class AddFillUpMethod {

            [Fact]
            public void CanAddNewFillUp() {
                var c = new Car();

                FillUp f = c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);

                Assert.Equal(1, c.FillUps.Count());
                Assert.Equal(1000, f.Odometer);
                Assert.Equal(50.0, f.Liters);
                Assert.True(f.IsFull);
            }

            [Fact]
            public void AddTwoFillUps() {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);
                FillUp f2 = c.AddFillUp(odometer: 2000, liters: 50.0, isFull: true);

                Assert.Equal(2, c.FillUps.Count());
                Assert.Equal(f1.NextFillUp, f2);
                Assert.Equal(f1, f2.PreviousFillUp);



            }

            [Fact]
            public void AddThreeFillUps() {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                FillUp f2 = c.AddFillUp(odometer: 1500,
                                       liters: 40.0,
                                       isFull: true);

                FillUp f3 = c.AddFillUp(odometer: 2100,
                                       liters: 56.0,
                                       isFull: true);

                Assert.Equal(3, c.FillUps.Count());
                Assert.Same(f2, f1.NextFillUp);
                Assert.Same(f3, f2.NextFillUp);

                Assert.Same(f1, f2.PreviousFillUp);
            }


        }

        public class AverageKilometersPerLiterProperty {

            [Fact]
            public void NoFillUp_IsNull() {
                var c = new Car();

                Assert.Null(c.AverageKilometersPerLiter);
            }

            [Fact]
            public void SingleFillUp_IsNull() {
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                Assert.Null(c.AverageKilometersPerLiter);
            }

            [Fact]
            public void TwoFillUps() {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                FillUp f2 = c.AddFillUp(odometer: 1600,
                                       liters: 60.0,
                                       isFull: true);



                Assert.Equal(10.0, c.AverageKilometersPerLiter);
            }

            [Fact]
            public void ThreeFillUps() {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                FillUp f2 = c.AddFillUp(odometer: 1600,
                                       liters: 60.0,
                                       isFull: true);

                FillUp f3 = c.AddFillUp(odometer: 2000,
                                       liters: 50.0,
                                       isFull: true);

                double? kml = c.AverageKilometersPerLiter;

                Assert.Equal(9.1, kml);
            }
        }

        public class AverageKmlWhenForgotFillUps {

            [Fact]
            public void OneBlock() {
                var c = new Car();

                c.AddFillUp(1000, 50, true, isForgot: false);
                c.AddFillUp(1600, 60, true, true);
                c.AddFillUp(2000, 50, true);
                c.AddFillUp(2600, 60, true);
                c.AddFillUp(3500, 50, true, true);

                var kml = c.AverageKilometersPerLiter;

                Assert.Equal(9.1, kml);

            }

            [Fact]
            public void TwoBlocks() {
                var c = new Car();

                c.AddFillUp(1000, 50);
                c.AddFillUp(1600, 60);
                c.AddFillUp(2000, 50);

                c.AddFillUp(4000, 50, isForgot:true);
                c.AddFillUp(4600,50);
                c.AddFillUp(5000, 50);

                var kml = c.AverageKilometersPerLiter;

                Assert.Equal(9.6, kml);

            }


        }

    }
}