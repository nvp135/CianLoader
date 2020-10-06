using System;
using System.Collections.Generic;
using CianLib.Model;

namespace CianLib
{
    public class City
    {
        public string CityName { get; set; }
        public int CityId { get; set; }
        public Coordinate UpperCorner { get; set; }
        public Coordinate LowerCorner { get; set; }
        public float Step { get; set; }
        public bool SaveToBase { get; set; }
        public bool SaveToCSV { get; set; }
        public List<string> DealTypes { get; set; }

        public List<List<Offer>> CityOffers = new List<List<Offer>>();
    }
}
