using System;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.DAL
{
    public class UnitOfWork : IDisposable
    {
        private PlacowkaZdrowiaContext context = new PlacowkaZdrowiaContext();
        private GenericRepository<Dzial> dzialRepository;
        private ZabiegRepository zabiegRepository;

        public GenericRepository<Dzial> DzialRepository
        {
            get
            {

                if (this.dzialRepository == null)
                {
                    this.dzialRepository = new GenericRepository<Dzial>(context);
                }
                return dzialRepository;
            }
        }

        public ZabiegRepository ZabiegRepository
        {
            get
            {

                if (this.zabiegRepository == null)
                {
                    this.zabiegRepository = new ZabiegRepository(context);
                }
                return zabiegRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}