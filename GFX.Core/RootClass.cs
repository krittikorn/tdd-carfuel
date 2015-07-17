using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GFX.Core {
    public abstract class RootClass : IDisposable {

        public DbContext Context { get; set; }
        private Dictionary<Type, object> _Services;


        public RootClass(bool testing = false) {
            this.Context = this.NewDbContext();
            this._Services = new Dictionary<Type, object>();

            if (testing) {
                RegisterServicesForUnitTests();
            }
            else {
                RegisterServices();
            }
        }

        public virtual void Dispose() {
            foreach (var item in _Services) {
                if (item.Value != null && item.Value is IDisposable) {
                    ((IDisposable)(item.Value)).Dispose();
                }
            }
            if (Context != null) Context.Dispose();
        }

        protected abstract DbContext NewDbContext();

        protected abstract void RegisterServices();
        protected abstract void RegisterServicesForUnitTests();

        public int SaveChanges() {
            if (Context == null) return -1;
            return Context.SaveChanges();
        }

        protected void AddService<TModel, TService, TRepository>()
            where TModel : class
            where TService : class, IService<TModel>, new()
            where TRepository : class, IRepository<TModel>, new() {

            Type key = typeof(TService);

            if (!_Services.ContainsKey(key)) {

                Lazy<IService<TModel>> obj;
                obj = new Lazy<IService<TModel>>(valueFactory: () => {
                    IService<TModel> x = new TService();  // T x = Activator.CreateInstance<T>(); 
                    x.Root = this;

                    if (x.RequiresOwnDbContext) {
                        x.Context = this.NewDbContext();
                    }
                    else {
                        x.Context = this.Context; // shared
                    }

                    x.Repository = new TRepository();
                    x.Repository.Context = this.Context;
                    return x;
                });

                _Services.Add(key: key, value: obj);
            }
        }

        public TService Services<TModel, TService>()
            where TModel : class
            where TService : class, IService<TModel> {

            Type key = typeof(TService);

            if (_Services.ContainsKey(key)) {
                var lazy = (Lazy<IService<TModel>>)_Services[key];
                return lazy.Value as TService;
            }
            else {
                var s = string.Format("Service '{0}' has not been registered to school.",
                  typeof(TService).Name);
                throw new Exception(s);
            }
        }

    }
}