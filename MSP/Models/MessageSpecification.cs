using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSP.Models
{
    public class MessageSpecification
    {
        public MessageSpecification()
        {
            Specification = new List<Object>();
        }

        [Key]
        public string Name { get; set; }

        public List<Object> Specification { get; set; }
    }
}