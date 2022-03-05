﻿namespace Contact.API.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
        public int InfoTypeId { get; set; }
        public InfoType? InfoType { get; set; }
        public string? Value { get; set; }
    }
}
