using System.Runtime.Serialization;

namespace PurchaseOrderService.DataContract
{
    [DataContract]
    public class Item
    {
        string _code = "";
        string _description = "";
        decimal _rate = 0;

        [DataMember]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [DataMember]
        public decimal Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
    }
}
