using System;
using System.ComponentModel.DataAnnotations;

namespace CianLib.Model
{

    public class OffersResponse
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string dataType { get; set; }
        public Offer[] offers { get; set; }
        public object villages { get; set; }
        public int itemsCount { get; set; }
        public Bbox bbox { get; set; }
    }

    public class Bbox
    {
        public Uppercorner upperCorner { get; set; }
        public Lowercorner lowerCorner { get; set; }
    }

    public class Uppercorner
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Lowercorner
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class BaseOffer
    {
        [Key]
        public long row_id { get; set; }
        public DateTime insert_date { get; set; }
        public int city { get; set; }
        public bool soft_deleted { get; set; }
    }

    public class Offer : BaseOffer
    {
        public int cian_id { get; set; }
        public string category { get; set; }
        public int? village_id { get; set; }
        public int added { get; set; }
        public int? house_id { get; set; }
        public int? newobject_id { get; set; }
        public string photo { get; set; }
        public long price { get; set; }
        public int? object_type { get; set; }
        public float lon { get; set; }
        public string filter_type { get; set; }
        public DateTime creation_date { get; set; }
        public string deal_type { get; set; }
        public bool from_developer { get; set; }
        public float lat { get; set; }
        public int service_id { get; set; }
        public int property_type { get; set; }
        public string id { get; set; }
        public int type { get; set; }

        public override string ToString()
        {
            return $"{cian_id},{category},{village_id},{added},{house_id},{newobject_id},{photo},{price},{object_type},{lon},{filter_type},{creation_date},{deal_type},{from_developer},{lat},{service_id},{property_type},{id},{type},{city},{insert_date.ToShortDateString()}";
        }
    }
}
