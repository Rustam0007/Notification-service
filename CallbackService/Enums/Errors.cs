using System.ComponentModel;

namespace CallbackService.Enums;

public enum Errors
{
    [Description("Approved")] 
    Approved = 1000,
    [Description("Bad request")] 
    BadRequest = 910,
    [Description("Not found")] 
    NotFound = 919,
    [Description("Internal server error")] 
    InternalError = 924,
    [Description("Gateway timeout")] 
    GatewayTimeout = 925
}