using System.Collections.Generic;

namespace FreightTech.Data.DataTransferObject
{
    public class CustomerDTO : UserProfileDTO
    {
        public int CustomerId { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
