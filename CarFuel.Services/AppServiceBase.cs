using GFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Services {

    public abstract class AppServiceBase<T> : ServiceBase<T>
      where T : class {

        protected App App {
            get {
                return this.Root as App;
            }
        }

    }
}