using CarFuel.DataAccess.Context;
using CarFuel.DataAccess.Repositories;
using CarFuel.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarFuel.Services {
    public class CarService {

        private readonly CarRepository repo;
        public CarService()
            : this(new CarRepository()) {
            //
        }

        public CarService(CarRepository repo) {
            this.repo = repo;
            this.repo.Context = new CarDb();
        }

        public IEnumerable<Car> GetAll() {
            return repo.Query(c => true).ToList();
        }

        public Car Add(Car item) {
            var c = repo.Add(item);
            repo.SaveChanges();
            return c;
        }


    }
}