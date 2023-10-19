
namespace To_Do_List.Models
{
    public class Response
    {

        public required string Status { set; get; }
        public required string Message { set; get; }
        public required string Email { set; get; }
        public required string Phone { set; get; }
        public required string Username { set; get; }
        public int Id { set; get; }
        public required string Token { set; get; }

    }

}
