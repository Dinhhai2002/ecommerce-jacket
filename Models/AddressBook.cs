using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webecommerce.Models
{
    [Table("AddressBooks")]
    public class AddressBook : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [StringLength(100)]
        public string CityName { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        [StringLength(100)]
        public string DistrictName { get; set; }

        [Required]
        public int WardId { get; set; }

        [Required]
        [StringLength(100)]
        public string WardName { get; set; }

        [Required]
        [StringLength(255)]
        public string FullAddress { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        public new int Status { get; set; } = 1;

        public AddressBook()
        {
        }
    }
} 