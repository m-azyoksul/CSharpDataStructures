namespace Qc.Result
{
    public enum ServiceError
    {
        ServiceFailure,
        InvalidInput,
        InvalidRequest,
        Unauthorized,
        DataToBeUpdatedIsTheSame,
        NotFound,
        
        /// <summary>
        /// 409 Conflict
        /// A request conflicts with the current state of the service.
        /// A change since the client last fetched the resource is causing a conflict.
        /// </summary>
        Conflict,
        
        PaymentFailed,
        ExternalServiceUnavailable

    }
}
