using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime UserAge { get; set; }
        public const string CoockieName = "name";
    }
}