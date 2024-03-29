﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhaseTicket.DeviceCommanderRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DeviceCommanderRef.ICommandService")]
    public interface ICommandService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/EnableMoneyReceiving", ReplyAction="http://tempuri.org/ICommandService/EnableMoneyReceivingResponse")]
        void EnableMoneyReceiving();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/EnableMoneyReceiving", ReplyAction="http://tempuri.org/ICommandService/EnableMoneyReceivingResponse")]
        System.Threading.Tasks.Task EnableMoneyReceivingAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/DisableMoneyReceiving", ReplyAction="http://tempuri.org/ICommandService/DisableMoneyReceivingResponse")]
        void DisableMoneyReceiving();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/DisableMoneyReceiving", ReplyAction="http://tempuri.org/ICommandService/DisableMoneyReceivingResponse")]
        System.Threading.Tasks.Task DisableMoneyReceivingAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/RecyclerStoredCount", ReplyAction="http://tempuri.org/ICommandService/RecyclerStoredCountResponse")]
        int RecyclerStoredCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/RecyclerStoredCount", ReplyAction="http://tempuri.org/ICommandService/RecyclerStoredCountResponse")]
        System.Threading.Tasks.Task<int> RecyclerStoredCountAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/GetSessionMoneyReceived", ReplyAction="http://tempuri.org/ICommandService/GetSessionMoneyReceivedResponse")]
        int GetSessionMoneyReceived();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/GetSessionMoneyReceived", ReplyAction="http://tempuri.org/ICommandService/GetSessionMoneyReceivedResponse")]
        System.Threading.Tasks.Task<int> GetSessionMoneyReceivedAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ReturnSessionMoney", ReplyAction="http://tempuri.org/ICommandService/ReturnSessionMoneyResponse")]
        void ReturnSessionMoney();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ReturnSessionMoney", ReplyAction="http://tempuri.org/ICommandService/ReturnSessionMoneyResponse")]
        System.Threading.Tasks.Task ReturnSessionMoneyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/FlushPaymentSession", ReplyAction="http://tempuri.org/ICommandService/FlushPaymentSessionResponse")]
        void FlushPaymentSession();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/FlushPaymentSession", ReplyAction="http://tempuri.org/ICommandService/FlushPaymentSessionResponse")]
        System.Threading.Tasks.Task FlushPaymentSessionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ClearRecycler", ReplyAction="http://tempuri.org/ICommandService/ClearRecyclerResponse")]
        void ClearRecycler();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ClearRecycler", ReplyAction="http://tempuri.org/ICommandService/ClearRecyclerResponse")]
        System.Threading.Tasks.Task ClearRecyclerAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/Change", ReplyAction="http://tempuri.org/ICommandService/ChangeResponse")]
        void Change(int notesToSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/Change", ReplyAction="http://tempuri.org/ICommandService/ChangeResponse")]
        System.Threading.Tasks.Task ChangeAsync(int notesToSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/PrintUrl", ReplyAction="http://tempuri.org/ICommandService/PrintUrlResponse")]
        void PrintUrl(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/PrintUrl", ReplyAction="http://tempuri.org/ICommandService/PrintUrlResponse")]
        System.Threading.Tasks.Task PrintUrlAsync(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ShowVKP80DriverSetup", ReplyAction="http://tempuri.org/ICommandService/ShowVKP80DriverSetupResponse")]
        void ShowVKP80DriverSetup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/ShowVKP80DriverSetup", ReplyAction="http://tempuri.org/ICommandService/ShowVKP80DriverSetupResponse")]
        System.Threading.Tasks.Task ShowVKP80DriverSetupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/PrintCheck", ReplyAction="http://tempuri.org/ICommandService/PrintCheckResponse")]
        void PrintCheck(decimal amount, decimal comission);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICommandService/PrintCheck", ReplyAction="http://tempuri.org/ICommandService/PrintCheckResponse")]
        System.Threading.Tasks.Task PrintCheckAsync(decimal amount, decimal comission);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICommandServiceChannel : PhaseTicket.DeviceCommanderRef.ICommandService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CommandServiceClient : System.ServiceModel.ClientBase<PhaseTicket.DeviceCommanderRef.ICommandService>, PhaseTicket.DeviceCommanderRef.ICommandService {
        
        public CommandServiceClient() {
        }
        
        public CommandServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CommandServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void EnableMoneyReceiving() {
            base.Channel.EnableMoneyReceiving();
        }
        
        public System.Threading.Tasks.Task EnableMoneyReceivingAsync() {
            return base.Channel.EnableMoneyReceivingAsync();
        }
        
        public void DisableMoneyReceiving() {
            base.Channel.DisableMoneyReceiving();
        }
        
        public System.Threading.Tasks.Task DisableMoneyReceivingAsync() {
            return base.Channel.DisableMoneyReceivingAsync();
        }
        
        public int RecyclerStoredCount() {
            return base.Channel.RecyclerStoredCount();
        }
        
        public System.Threading.Tasks.Task<int> RecyclerStoredCountAsync() {
            return base.Channel.RecyclerStoredCountAsync();
        }
        
        public int GetSessionMoneyReceived() {
            return base.Channel.GetSessionMoneyReceived();
        }
        
        public System.Threading.Tasks.Task<int> GetSessionMoneyReceivedAsync() {
            return base.Channel.GetSessionMoneyReceivedAsync();
        }
        
        public void ReturnSessionMoney() {
            base.Channel.ReturnSessionMoney();
        }
        
        public System.Threading.Tasks.Task ReturnSessionMoneyAsync() {
            return base.Channel.ReturnSessionMoneyAsync();
        }
        
        public void FlushPaymentSession() {
            base.Channel.FlushPaymentSession();
        }
        
        public System.Threading.Tasks.Task FlushPaymentSessionAsync() {
            return base.Channel.FlushPaymentSessionAsync();
        }
        
        public void ClearRecycler() {
            base.Channel.ClearRecycler();
        }
        
        public System.Threading.Tasks.Task ClearRecyclerAsync() {
            return base.Channel.ClearRecyclerAsync();
        }
        
        public void Change(int notesToSend) {
            base.Channel.Change(notesToSend);
        }
        
        public System.Threading.Tasks.Task ChangeAsync(int notesToSend) {
            return base.Channel.ChangeAsync(notesToSend);
        }
        
        public void PrintUrl(string url) {
            base.Channel.PrintUrl(url);
        }
        
        public System.Threading.Tasks.Task PrintUrlAsync(string url) {
            return base.Channel.PrintUrlAsync(url);
        }
        
        public void ShowVKP80DriverSetup() {
            base.Channel.ShowVKP80DriverSetup();
        }
        
        public System.Threading.Tasks.Task ShowVKP80DriverSetupAsync() {
            return base.Channel.ShowVKP80DriverSetupAsync();
        }
        
        public void PrintCheck(decimal amount, decimal comission) {
            base.Channel.PrintCheck(amount, comission);
        }
        
        public System.Threading.Tasks.Task PrintCheckAsync(decimal amount, decimal comission) {
            return base.Channel.PrintCheckAsync(amount, comission);
        }
    }
}
