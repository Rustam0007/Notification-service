using System.ComponentModel;

namespace CallbackService.Enums;

public enum Errors
{
    [Description("Approved")] 
    Approved = 1000,

    [Description("Accepted for further process")]
    Accepted = 900,
    [Description("Approved with partial amount")]
    ApprovedPartially = 901,
    [Description("Accepted, need confirmation code")]
    AcceptedNeedConfirmationCode = 902,
    [Description("Invalid confirmation code")]
    InvalidConfirmationCode = 903,
    [Description("Amount on hold by host")]
    AmountOnHoldByHost = 904,

    [Description("Bad request")] 
    BadRequest = 910,
    [Description("Bad gateway")] 
    BadGateway = 911,
    [Description("Upstream service unavailable")]
    UpstreamServiceUnavailable = 912,
    [Description("Limit exceeded")] 
    LimitExceeded = 913,
    [Description("Invalid card/account")] 
    InvalidCard = 914,
    [Description("Insufficient funds")] 
    InsufficientFunds = 915,
    [Description("Invalid amount")] 
    InvalidAmount = 916,
    [Description("Invalid transaction")] 
    InvalidTransaction = 917,
    [Description("Can not finalize transaction")]
    CantFinalizeTransaction = 918,
    [Description("Not found")] 
    NotFound = 919,
    [Description("Sender card/account blocked or temporarily unavailable")]
    SenderUnavailable = 920,
    [Description("Receiver card/account blocked or temporarily unavailable")]
    ReceiverUnavailable = 921,
    [Description("Unauthorized client")] 
    UnauthorizedClient = 922,
    [Description("Forbidden transaction")] 
    ForbiddenTransaction = 923,
    [Description("Internal server error")] 
    InternalError = 924,
    [Description("Gateway timeout")] 
    GatewayTimeout = 925,
    [Description("Duplicate transaction")] 
    DuplicateTransaction = 926,
    [Description("Transaction declined by host")]
    TransactionDeclinedByHost = 927,
    [Description("Transaction on hold by host")]
    TransactionOnHold = 928,
    [Description("Debtor's card")]
    DebtorCard = 929
}