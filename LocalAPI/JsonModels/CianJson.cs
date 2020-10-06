using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalAPI.JsonModels
{
        public class CianJson
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

        public class Offer
        {
            public int cian_id { get; set; }
            public string category { get; set; }
            public int? village_id { get; set; }
            public int added { get; set; }
            public int? house_id { get; set; }
            public int? newobject_id { get; set; }
            public byte[] photo { get; set; }
            public float price { get; set; }
            public int object_type { get; set; }
            public float lon { get; set; }
            public string filter_type { get; set; }
            public DateTime creation_date { get; set; }
            public int?[] builders_ids { get; set; }
            public string deal_type { get; set; }
            public bool from_developer { get; set; }
            public float lat { get; set; }
            public int service_id { get; set; }
            public int property_type { get; set; }
            public string id { get; set; }
            public int type { get; set; }
            public int multi_count { get; set; }
            public int multioffers_count { get; set; }
        }
}
