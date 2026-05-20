using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "A teljes név megadása kötelező.")]
        [Display(Name = "Teljes név")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Az e-mail cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Érvénytelen e-mail formátum.")]
        [Display(Name = "E-mail cím")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [Display(Name = "Telefonszám")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "A város megadása kötelező.")]
        [Display(Name = "Város")]
        public string City { get; set; }

        [Required(ErrorMessage = "Az irányítószám kötelező.")]
        [Display(Name = "Irányítószám")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Az utca, házszám kötelező.")]
        [Display(Name = "Utca, házszám")]
        public string StreetAddress { get; set; }

        [Display(Name = "Végösszeg (Ft)")]
        public int TotalPrice { get; set; }

        [Display(Name = "Rendelés dátuma")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Státusz")]
        public string Status { get; set; } = "Feldolgozás alatt";

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}