using CarFuel.DataAccess.Repositories;
using CarFuel.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarFuel.Services {
    public class CarService {

        private readonly CarRepository repo;

        public CarService(CarRepository repo) {
            this.repo = repo;
        }

        public IEnumerable<Car> GetAll() {
            return repo.Query(c => true).ToList();
        }

        public Car Add(Car item) {
            return repo.Add(item);
        }

    }
}