using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.DAL
{
    public interface IPacjentRepository : IDisposable
    {
        IEnumerable<Pacjent> GetPacjenci();
        Pacjent GetPacjentByID(int pacjentId);
        void InsertPacjent(Pacjent pacjent);
        void DeletePacjent(int pacjentId);
        void UpdatePacjent(Pacjent pacjent);
        void Save();
    }
}