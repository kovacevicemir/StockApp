﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockApp.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/StockWcf")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Member", Namespace="http://schemas.datacontract.org/2004/07/StockWcf")]
    [System.SerializableAttribute()]
    public partial class Member : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string emailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string firstnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string lastSearchAllField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string lastSearchHistoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string lastSearchLiveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string lastnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string passwordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string email {
            get {
                return this.emailField;
            }
            set {
                if ((object.ReferenceEquals(this.emailField, value) != true)) {
                    this.emailField = value;
                    this.RaisePropertyChanged("email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string firstname {
            get {
                return this.firstnameField;
            }
            set {
                if ((object.ReferenceEquals(this.firstnameField, value) != true)) {
                    this.firstnameField = value;
                    this.RaisePropertyChanged("firstname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string lastSearchAll {
            get {
                return this.lastSearchAllField;
            }
            set {
                if ((object.ReferenceEquals(this.lastSearchAllField, value) != true)) {
                    this.lastSearchAllField = value;
                    this.RaisePropertyChanged("lastSearchAll");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string lastSearchHistory {
            get {
                return this.lastSearchHistoryField;
            }
            set {
                if ((object.ReferenceEquals(this.lastSearchHistoryField, value) != true)) {
                    this.lastSearchHistoryField = value;
                    this.RaisePropertyChanged("lastSearchHistory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string lastSearchLive {
            get {
                return this.lastSearchLiveField;
            }
            set {
                if ((object.ReferenceEquals(this.lastSearchLiveField, value) != true)) {
                    this.lastSearchLiveField = value;
                    this.RaisePropertyChanged("lastSearchLive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string lastname {
            get {
                return this.lastnameField;
            }
            set {
                if ((object.ReferenceEquals(this.lastnameField, value) != true)) {
                    this.lastnameField = value;
                    this.RaisePropertyChanged("lastname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                if ((object.ReferenceEquals(this.passwordField, value) != true)) {
                    this.passwordField = value;
                    this.RaisePropertyChanged("password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Nsye", Namespace="http://schemas.datacontract.org/2004/07/StockWcf")]
    [System.SerializableAttribute()]
    public partial class Nsye : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string date_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string date_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string exchangeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_adj_close_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_adj_close_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_close_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_close_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_high_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_high_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_low_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_low_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_open_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float stock_price_open_toField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string stock_symbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int stock_volume_fromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int stock_volume_toField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string date_from {
            get {
                return this.date_fromField;
            }
            set {
                if ((object.ReferenceEquals(this.date_fromField, value) != true)) {
                    this.date_fromField = value;
                    this.RaisePropertyChanged("date_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string date_to {
            get {
                return this.date_toField;
            }
            set {
                if ((object.ReferenceEquals(this.date_toField, value) != true)) {
                    this.date_toField = value;
                    this.RaisePropertyChanged("date_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string exchange {
            get {
                return this.exchangeField;
            }
            set {
                if ((object.ReferenceEquals(this.exchangeField, value) != true)) {
                    this.exchangeField = value;
                    this.RaisePropertyChanged("exchange");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_adj_close_from {
            get {
                return this.stock_price_adj_close_fromField;
            }
            set {
                if ((this.stock_price_adj_close_fromField.Equals(value) != true)) {
                    this.stock_price_adj_close_fromField = value;
                    this.RaisePropertyChanged("stock_price_adj_close_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_adj_close_to {
            get {
                return this.stock_price_adj_close_toField;
            }
            set {
                if ((this.stock_price_adj_close_toField.Equals(value) != true)) {
                    this.stock_price_adj_close_toField = value;
                    this.RaisePropertyChanged("stock_price_adj_close_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_close_from {
            get {
                return this.stock_price_close_fromField;
            }
            set {
                if ((this.stock_price_close_fromField.Equals(value) != true)) {
                    this.stock_price_close_fromField = value;
                    this.RaisePropertyChanged("stock_price_close_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_close_to {
            get {
                return this.stock_price_close_toField;
            }
            set {
                if ((this.stock_price_close_toField.Equals(value) != true)) {
                    this.stock_price_close_toField = value;
                    this.RaisePropertyChanged("stock_price_close_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_high_from {
            get {
                return this.stock_price_high_fromField;
            }
            set {
                if ((this.stock_price_high_fromField.Equals(value) != true)) {
                    this.stock_price_high_fromField = value;
                    this.RaisePropertyChanged("stock_price_high_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_high_to {
            get {
                return this.stock_price_high_toField;
            }
            set {
                if ((this.stock_price_high_toField.Equals(value) != true)) {
                    this.stock_price_high_toField = value;
                    this.RaisePropertyChanged("stock_price_high_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_low_from {
            get {
                return this.stock_price_low_fromField;
            }
            set {
                if ((this.stock_price_low_fromField.Equals(value) != true)) {
                    this.stock_price_low_fromField = value;
                    this.RaisePropertyChanged("stock_price_low_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_low_to {
            get {
                return this.stock_price_low_toField;
            }
            set {
                if ((this.stock_price_low_toField.Equals(value) != true)) {
                    this.stock_price_low_toField = value;
                    this.RaisePropertyChanged("stock_price_low_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_open_from {
            get {
                return this.stock_price_open_fromField;
            }
            set {
                if ((this.stock_price_open_fromField.Equals(value) != true)) {
                    this.stock_price_open_fromField = value;
                    this.RaisePropertyChanged("stock_price_open_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float stock_price_open_to {
            get {
                return this.stock_price_open_toField;
            }
            set {
                if ((this.stock_price_open_toField.Equals(value) != true)) {
                    this.stock_price_open_toField = value;
                    this.RaisePropertyChanged("stock_price_open_to");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string stock_symbol {
            get {
                return this.stock_symbolField;
            }
            set {
                if ((object.ReferenceEquals(this.stock_symbolField, value) != true)) {
                    this.stock_symbolField = value;
                    this.RaisePropertyChanged("stock_symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int stock_volume_from {
            get {
                return this.stock_volume_fromField;
            }
            set {
                if ((this.stock_volume_fromField.Equals(value) != true)) {
                    this.stock_volume_fromField = value;
                    this.RaisePropertyChanged("stock_volume_from");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int stock_volume_to {
            get {
                return this.stock_volume_toField;
            }
            set {
                if ((this.stock_volume_toField.Equals(value) != true)) {
                    this.stock_volume_toField = value;
                    this.RaisePropertyChanged("stock_volume_to");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        StockApp.ServiceReference1.CompositeType GetDataUsingDataContract(StockApp.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<StockApp.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(StockApp.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMember", ReplyAction="http://tempuri.org/IService1/GetMemberResponse")]
        StockApp.ServiceReference1.Member GetMember(StockApp.ServiceReference1.Member m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMember", ReplyAction="http://tempuri.org/IService1/GetMemberResponse")]
        System.Threading.Tasks.Task<StockApp.ServiceReference1.Member> GetMemberAsync(StockApp.ServiceReference1.Member m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/FetchProducts", ReplyAction="http://tempuri.org/IService1/FetchProductsResponse")]
        System.Data.DataTable FetchProducts(string query);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/FetchProducts", ReplyAction="http://tempuri.org/IService1/FetchProductsResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> FetchProductsAsync(string query);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SearchData", ReplyAction="http://tempuri.org/IService1/SearchDataResponse")]
        System.Data.DataTable SearchData(StockApp.ServiceReference1.Nsye nsye);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SearchData", ReplyAction="http://tempuri.org/IService1/SearchDataResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> SearchDataAsync(StockApp.ServiceReference1.Nsye nsye);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/lastSearch", ReplyAction="http://tempuri.org/IService1/lastSearchResponse")]
        int lastSearch([System.ServiceModel.MessageParameterAttribute(Name="lastSearch")] string lastSearch1, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/lastSearch", ReplyAction="http://tempuri.org/IService1/lastSearchResponse")]
        System.Threading.Tasks.Task<int> lastSearchAsync(string lastSearch, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/selectHeaders", ReplyAction="http://tempuri.org/IService1/selectHeadersResponse")]
        System.Data.DataTable selectHeaders(string tableName, string header);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/selectHeaders", ReplyAction="http://tempuri.org/IService1/selectHeadersResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> selectHeadersAsync(string tableName, string header);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/updateCompare", ReplyAction="http://tempuri.org/IService1/updateCompareResponse")]
        int updateCompare(string Compare, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/updateCompare", ReplyAction="http://tempuri.org/IService1/updateCompareResponse")]
        System.Threading.Tasks.Task<int> updateCompareAsync(string Compare, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/insertCompare", ReplyAction="http://tempuri.org/IService1/insertCompareResponse")]
        int insertCompare(string Compare, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/insertCompare", ReplyAction="http://tempuri.org/IService1/insertCompareResponse")]
        System.Threading.Tasks.Task<int> insertCompareAsync(string Compare, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMemberByUsername", ReplyAction="http://tempuri.org/IService1/GetMemberByUsernameResponse")]
        System.Data.DataTable GetMemberByUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMemberByUsername", ReplyAction="http://tempuri.org/IService1/GetMemberByUsernameResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetMemberByUsernameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/insertMember", ReplyAction="http://tempuri.org/IService1/insertMemberResponse")]
        int insertMember(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/insertMember", ReplyAction="http://tempuri.org/IService1/insertMemberResponse")]
        System.Threading.Tasks.Task<int> insertMemberAsync(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : StockApp.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<StockApp.ServiceReference1.IService1>, StockApp.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public StockApp.ServiceReference1.CompositeType GetDataUsingDataContract(StockApp.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<StockApp.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(StockApp.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public StockApp.ServiceReference1.Member GetMember(StockApp.ServiceReference1.Member m) {
            return base.Channel.GetMember(m);
        }
        
        public System.Threading.Tasks.Task<StockApp.ServiceReference1.Member> GetMemberAsync(StockApp.ServiceReference1.Member m) {
            return base.Channel.GetMemberAsync(m);
        }
        
        public System.Data.DataTable FetchProducts(string query) {
            return base.Channel.FetchProducts(query);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> FetchProductsAsync(string query) {
            return base.Channel.FetchProductsAsync(query);
        }
        
        public System.Data.DataTable SearchData(StockApp.ServiceReference1.Nsye nsye) {
            return base.Channel.SearchData(nsye);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> SearchDataAsync(StockApp.ServiceReference1.Nsye nsye) {
            return base.Channel.SearchDataAsync(nsye);
        }
        
        public int lastSearch(string lastSearch1, int userId) {
            return base.Channel.lastSearch(lastSearch1, userId);
        }
        
        public System.Threading.Tasks.Task<int> lastSearchAsync(string lastSearch, int userId) {
            return base.Channel.lastSearchAsync(lastSearch, userId);
        }
        
        public System.Data.DataTable selectHeaders(string tableName, string header) {
            return base.Channel.selectHeaders(tableName, header);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> selectHeadersAsync(string tableName, string header) {
            return base.Channel.selectHeadersAsync(tableName, header);
        }
        
        public int updateCompare(string Compare, int userId) {
            return base.Channel.updateCompare(Compare, userId);
        }
        
        public System.Threading.Tasks.Task<int> updateCompareAsync(string Compare, int userId) {
            return base.Channel.updateCompareAsync(Compare, userId);
        }
        
        public int insertCompare(string Compare, int userId) {
            return base.Channel.insertCompare(Compare, userId);
        }
        
        public System.Threading.Tasks.Task<int> insertCompareAsync(string Compare, int userId) {
            return base.Channel.insertCompareAsync(Compare, userId);
        }
        
        public System.Data.DataTable GetMemberByUsername(string username) {
            return base.Channel.GetMemberByUsername(username);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetMemberByUsernameAsync(string username) {
            return base.Channel.GetMemberByUsernameAsync(username);
        }
        
        public int insertMember(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch) {
            return base.Channel.insertMember(nickname, fname, lname, email, password, allSearch, historySearch, liveSearch);
        }
        
        public System.Threading.Tasks.Task<int> insertMemberAsync(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch) {
            return base.Channel.insertMemberAsync(nickname, fname, lname, email, password, allSearch, historySearch, liveSearch);
        }
    }
}
