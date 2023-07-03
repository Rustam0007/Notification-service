namespace CallbackService;

public interface ISmsClient
{
    public void SendByPhoneNumber(string phone, string data, string operationType, string card, string resourse);
}