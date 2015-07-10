using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.DataAccess.Context {
    class CarDb : DbContext {

        public DbSet Cars { get; set; }
        public DbSet FillUps { get; set; }

    }
}
