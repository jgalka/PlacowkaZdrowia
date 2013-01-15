using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.DAL
{
    public class PacjentRepository : IPacjentRepository, IDisposable
    {
        private PlacowkaZdrowiaContext context;

        public PacjentRepository(PlacowkaZdrowiaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Pacjent> GetPacjenci()
        {
            return context.Pacjenci.ToList();
        }

        public Pacjent GetPacjentByID(int id)
        {
            return context.Pacjenci.Find(id);
        }

        public void InsertPacjent(Pacjent pacjent)
        {
            context.Pacjenci.Add(pacjent);
        }

        public void DeletePacjent(int pacjentID)
        {
            Pacjent pacjent = context.Pacjenci.Find(pacjentID);
            context.Pacjenci.Remove(pacjent);
        }

        public void UpdatePacjent(Pacjent pacjent)
        {
            context.Entry(pacjent).State = EntityState.Modified;
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