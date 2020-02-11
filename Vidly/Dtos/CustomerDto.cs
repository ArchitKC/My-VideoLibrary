using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string customerName { get; set; }

        public bool IsSubscriberToNewsLetter { get; set; }

        public MemberShipTypeDto MemberShipType { get; set; }
        public byte MemberShipTypeId { get; set; }

        //[Min18yearValidation]
        public DateTime? BirthDate { get; set; }
    }
}