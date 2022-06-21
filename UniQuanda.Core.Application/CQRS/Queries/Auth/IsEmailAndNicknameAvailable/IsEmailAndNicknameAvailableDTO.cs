﻿using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable
{
    public class IsEmailAndNicknameAvailableRequestDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(32)]
        public string Nickname { get; set; }
    }

    public class IsEmailAndNicknameAvailableResponseDTO
    {
        public bool IsEmailAvailable { get; set; }
        public bool IsNicknameAvailable { get; set; }
    }
}