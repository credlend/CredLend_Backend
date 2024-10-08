﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredLend.Domain.DTOs
{
    public class RegisterResponseDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? CompleteName { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public bool IsSucceded { get; set; }
        public bool UserAlreadyExists { get; set; }
        public bool ExternalLogin { get; set; }
    }
}
