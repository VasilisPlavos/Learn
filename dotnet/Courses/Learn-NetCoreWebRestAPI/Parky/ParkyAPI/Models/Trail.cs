﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }

        public enum DifficultyType {Easy, Moderate, Difficult, Expert }
        public  DifficultyType Difficulty { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public  int NationalParkId { get; set; }

        [ForeignKey("NationalParkId")]
        public  NationalPark NationalPark { get; set; }
    }
}
