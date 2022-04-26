using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    [BindProperties]
    public class Promotion
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public Promotion()
        {

        }
    }
}
