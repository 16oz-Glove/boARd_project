public class Message
{
    public string sender;
    public string receiver;
    public string date;
    public string content;

    public Message(string sender, string receiver, string date, string content) // 생성자 사용하여 초기화
    {
        this.sender = sender;
        this.receiver = receiver;
        this.date = date;
        this.content = content;
    }
}
