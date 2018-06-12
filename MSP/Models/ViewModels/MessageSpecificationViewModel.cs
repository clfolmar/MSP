using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSP.Models.ViewModels
{
    public class MessageSpecificationViewModel
    {
        //public MessageSpecificationViewModel()
        //{
        //    Specification = new List<string>();
        //}

        public string Name { get; set; }

        public IEnumerable<string> Specification { get; set; }
    }
}