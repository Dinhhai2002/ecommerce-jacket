namespace webecommerce.Models.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Birthday { get; set; }
        public int WardId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public string FullAddress { get; set; }
        public int Role { get; set; }
        public UserResponse() {}
        public UserResponse(webecommerce.Data.User user)
        {
            Id = user.Id;
            UserName = user.Username;
            FullName = user.FullName;
            Email = user.Email;
            Phone = user.Phone;
            Gender = user.Gender;
            Birthday = user.Birthday;
            WardId = user.WardId;
            DistrictId = user.DistrictId;
            CityId = user.CityId;
            FullAddress = user.FullAddress;
            Role = user.Role;
        }
    }
} 