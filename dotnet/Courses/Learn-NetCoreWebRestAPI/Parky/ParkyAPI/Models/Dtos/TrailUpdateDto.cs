﻿using System;
using System.ComponentModel.DataAnnotations;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.Dtos
{
    public class TrailUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public int NationalParkId { get; set; }

    }
}
