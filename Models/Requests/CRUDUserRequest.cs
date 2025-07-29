namespace webecommerce.Models.Requests
{
    public class CRUDUserRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public string Birthday { get; set; }
        public int WardId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public string FullAddress { get; set; }
        public int Role { get; set; }
    }
} 