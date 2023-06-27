namespace CallbackService;

public class SmsClient
{
    public static void SendByPhoneNumber(object phone, object data, object operationType, object card, object resourse)
    {
        Console.WriteLine($"\nОтправка смс на номер: {phone}\n");
        
        Console.Write($"Дата: {data}\n" +
                      $"Тип операции: {operationType}\n" +
                      $"Карта: {card}\n" +
                      $"Ресурс: {resourse}\n\n");
    }
}

// public class TelegramNotificationService : ITelegramNotificationService
// {
//     private readonly object _botToken;
//     private readonly long _chatId;
//
//     public TelegramNotificationService()
//     {
//         _botToken = "6056933504:AAE2kEZdfPmvB5Ac2JUsxoxgLRTZ4mUFL6M";
//         _chatId = 653572047;
//     }
//
//     public void Send(object message)
//     {
//         using (HttpClient client = new HttpClient())
//         {
//             object url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={message}";
//             HttpResponseMessage response = client.GetAsync(url).Result;
//
//             if (response.IsSuccessStatusCode)
//             {
//                 Console.WriteLine("Message sent successfully.");
//             }
//             else
//             {
//                 Console.WriteLine("Failed to send message. Error: " + response.StatusCode);
//             }
//         }
//     }
//     
// }

