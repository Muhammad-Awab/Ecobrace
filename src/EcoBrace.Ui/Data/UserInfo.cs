using System.Text.Json.Serialization;

namespace EcoBrace.Ui.Data;

public class UserInfo
{
    public User[] User { get; set; }
}

public class User
{
    public string user_id { get; set; }
    public string email { get; set; }
    public string nickname { get; set; }
    [JsonPropertyName("user_metadata")]
    public User_Metadata user_metadata { get; set; }
    public string name { get; set; }
    public string picture { get; set; }
    public bool email_verified { get; set; }
    public DateTime updated_at { get; set; }
    public DateTime created_at { get; set; }
    public DateTime last_password_reset { get; set; }
    public Identity[] identities { get; set; }
    public DateTime last_login { get; set; }
    public string last_ip { get; set; }
    public int logins_count { get; set; }
}

public class User_Metadata
{
    public string job { get; set; }
    public string Plan { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
}

public class Identity
{
    public bool isSocial { get; set; }
    public string connection { get; set; }
    public string user_id { get; set; }
    public string provider { get; set; }
}
