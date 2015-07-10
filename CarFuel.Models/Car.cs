using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CarFuel.Models {
    public class Car {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string Make { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [StringLength(30)]
        public string PlateNo { get; set; }

        public virtual ICollection<FillUp> FillUps { get; set; }
        public Car() {
            FillUps = new HashSet<FillUp>();
        }

        public FillUp AddFillUp(int odometer, double liters, bool isFull = true, bool isForgot = false) {

            var thisFillUp = new FillUp();

            thisFillUp.Odometer = odometer;
            thisFillUp.Liters = liters;
            thisFillUp.IsFull = isFull;
            thisFillUp.IsForgot = isForgot;

            var previousFillUp = this.FillUps.LastOrDefault();
            if (previousFillUp != null) {
                previousFillUp.NextFillUp = thisFillUp;
                thisFillUp.PreviousFillUp = previousFillUp;
            }

            this.FillUps.Add(thisFillUp);

            return thisFillUp;


        }

        public double? AverageKilometersPerLiter {
            get {
                if (FillUps.Count() < 2) {
                    return null;
                }

                FillUp first;
                FillUp last = FillUps.Last();
                double sumKML = 0.0;
                int blocks = 0;

                do {
                    while (last.IsForgot && last.PreviousFillUp != null) {
                        last = last.PreviousFillUp;
                    }

                    first = last;
                    double liters = 0.0;
                    while (first.PreviousFillUp != null) {
                        liters += first.Liters;
                        first = first.PreviousFillUp;
                        if (first.IsForgot) break;
                    }

                    var distance = last.Odometer - first.Odometer;

                    if (liters > 0) {
                        var kml = Math.Round(distance / liters, 1, MidpointRounding.AwayFromZero);
                        sumKML += kml;
                        blocks++;
                    }

                    last = first.PreviousFillUp;

                } while (last != null);

                return Math.Round(sumKML / blocks,1,MidpointRounding.AwayFromZero);
            }
        }
    }
}