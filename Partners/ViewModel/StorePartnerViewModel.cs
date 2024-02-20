using Partners.DataAccess.Models;
using Partners.DataAccess.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Partners.ViewModel
{
    public class StorePartnerViewModel
    {
        public PartnerSaveDto SavePartner { get; set; }
        public int Id { get; set; }
    }
}