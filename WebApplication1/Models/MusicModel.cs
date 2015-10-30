using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MusicModel
    {
        public DateTime creationDate { get; set; }
        public MusicModel()
        {
            creationDate = new DateTime(2015, 11, 11);
        }
        public DateTime timeLost {
            get
            {
                TimeSpan a = DateTime.Now - creationDate;
                DateTime asaaa = new DateTime(a.Ticks);
                return asaaa;
            }
        }
    }
}