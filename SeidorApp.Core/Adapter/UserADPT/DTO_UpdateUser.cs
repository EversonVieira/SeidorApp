namespace SeidorApp.Core.Adapter.UserAdapter
{
    public class DTO_UpdateUser : DTO_RegisterUser
    {
        public string? OldPassword { get; set; }
    }
}
