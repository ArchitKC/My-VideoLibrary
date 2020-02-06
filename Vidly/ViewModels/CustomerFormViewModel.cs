using System;
using System.Collections.Generic; 
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MemberShipType> MemberShipTypes { get; set; }
        public Customer Customer { get; set; }

        public string Title {
            get
            {
                if (Customer != null && Customer.Id != 0)
                    return "Edit Customer";
                else
                    return "New Customer";
            }
        }
    }
}