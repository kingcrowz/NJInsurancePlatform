﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NJInsurancePlatform.Models
{
    public class Policy
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid PolicyMUID { get; set; }

        [DisplayName("Policy Number")]
        [Required(ErrorMessage = "Missing - PolicyNumber")]
        public int PolicyNumber { get; set; }

        [DisplayName("Name Of Policy")]
        [Required(ErrorMessage = "Missing - Name Of Policy")]
        [StringLength(50)]
        public string? NameOfPolicy { get; set; }

        [DisplayName("Policy Owner")]
        [Required(ErrorMessage = "Missing - Policy Owner")]
        [StringLength(50)]
        public string? PolicyOwner { get; set; }

        [DisplayName("Policy Payment is Due")]
        public Boolean PolicyPaymentisDue { get; set; }

        [DisplayName("Policy Total Amount")]
        [Required(ErrorMessage = "Missing - Minimum Payment")]
        public double PolicyTotalAmount { get; set; }

        [DisplayName("Policy Paid Off Amount")]
        [Required(ErrorMessage = "Missing - Minimum Payment")]
        public double PolicyPaidOffAmount { get; set; }

        [DisplayName("Policy Start Date")]
        [Required(ErrorMessage = "Missing - Policy Due Date")]
        public DateTime PolicyStart_Date { get; set; }

        [DisplayName("Policy End Date")]
        [Required(ErrorMessage = "Missing - Policy End Date")]
        public DateTime PolicyEnd_Date { get; set; }

    }
}