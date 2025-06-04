﻿namespace Presentation.Models;

public class RegisterRequest
{
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string StreetName { get; set; } = null!;
        public string? StreetNumber { get; set; }
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
}

