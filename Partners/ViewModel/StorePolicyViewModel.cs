using Partners.DataAccess.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Partners.ViewModel
{
    public class StorePolicyViewModel
    {
        public PolicySaveDto SavePolicy { get; set; }
        public int Id { get; set; }
    }
}