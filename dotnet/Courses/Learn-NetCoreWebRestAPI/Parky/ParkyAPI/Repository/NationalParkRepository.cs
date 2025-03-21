﻿using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
       
        /// I need these to connect with the database
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db) { _db = db; }


        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(nationalPark => nationalPark.Id == nationalParkId);
        }

        public async Task<ICollection<NationalPark>> GetNationalParksAsync()
        {
            // this works also but we want them OrderByName
            // return _db.NationalParks.ToList();
            return await _db.NationalParks.OrderBy(nationalPark => nationalPark.Name).ToListAsync();
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(park => park.Name.ToLower().Trim() == name.ToLower().Trim() );
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(park => park.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
