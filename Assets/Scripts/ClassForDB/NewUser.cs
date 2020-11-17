// 클래스 세팅해두고 join안에 DB삽입 메서드 생성하는 버전(DB manager 없는 버전)
// User Document 세팅
public class NewUser
{
    public string email;
    public string nickName;
    public string regDate;

    public NewUser(string email, string nickName, string regDate) // 생성자 사용하여 초기화
    {
        this.email = email;
        this.nickName = nickName;
        this.regDate = regDate;
    }
}
