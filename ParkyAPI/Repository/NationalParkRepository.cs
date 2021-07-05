﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository( ApplicationDbContext db)
        {
            _db = db;
        }
        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(x=>x.Name).ToList();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
          return  _db.NationalParks.FirstOrDefault(x => x.Id == nationalParkId);
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            bool value = _db.NationalParks.Any(x => x.Id == id);
            return value;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public bool Save()
        {
            int count = _db.SaveChanges();
            if (count > 0)
            { 
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
