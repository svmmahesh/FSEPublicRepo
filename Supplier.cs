using System.Runtime.Serialization;

namespace PurchaseOrderService.DataContract
{
    [DataContract]
    public class Supplier
    {
        string _no = "";
        string _name = "";
        string _address = "";        
        
        [DataMember]
        public string No
        {
            get { return _no; }
            set { _no = value; }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }    
    }
}
