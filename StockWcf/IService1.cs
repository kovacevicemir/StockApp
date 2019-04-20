using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace StockWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        Member GetMember(Member m);

        [OperationContract]
        DataTable FetchProducts(string query);

        [OperationContract]
        DataTable SearchData(Nsye nsye);

        [OperationContract]
        int lastSearch(string lastSearch, int userId);

        [OperationContract]
        DataTable selectHeaders(string tableName, string header);

        [OperationContract]
        int updateCompare(string Compare, int userId);

        [OperationContract]
        int insertCompare(string Compare, int userId);

        [OperationContract]
        DataTable GetMemberByUsername(string username);

        [OperationContract]
        int insertMember(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch);


        //[OperationContract]
        //DataTable FetchProducts1(SqlCommand cmd5);
        //TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "StockWcf.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
