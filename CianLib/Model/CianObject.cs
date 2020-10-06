using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CianLib.Model
{
    public class CianObject
    {
        public int cian_id { get; set; }
        public int city { get; set; }
        public string category { get; set; }
        public int? village_id { get; set; }
        public int added { get; set; }
        public int? house_id { get; set; }
        public int? newobject_id { get; set; }
        public string photo { get; set; }

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

        public IEnumerable<CianObjectPrice> prices { get; set; }
        public bool soft_deleted { get; set; }
    }

    public class CianObjectPrice
    {
        [Key]
        public long row_id { get; set; }
        public DateTime insert_date { get; set; }
        public long price { get; set; }

        public int cian_id { get; set; }
        public int city { get; set; }
        public CianObject cian_object { get; set; }
    }
}
