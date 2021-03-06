﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hn.AutoSyncLib.Model
{
    public class MC_PickUpGoods_Result:MC_Request_BaseResult
    {
        public List<MC_PickUpGoods_ResultInfo> resultInfo { get; set; }
    }

    [Table("MN_THD")]
    public class MC_PickUpGoods_ResultInfo
    {
        [Column("TPACKAGE")]
        public string package { get; set; }
        public int autoId { get; set; }//
        public string pzhm { get; set; }//
        public string pjhm { get; set; }//
        public DateTime rq { get; set; }//
        public string khhm { get; set; }//
        public string khmc { get; set; }//
        public string cppz { get; set; }//
        public string cpxh { get; set; }//
        public string cpgg { get; set; }
        public string cpsh { get; set; }
        public string cpdj { get; set; }//
        public string cpcm { get; set; }//
        public string dw { get; set; }//
        public decimal ks { get; set; }//
        public decimal sl { get; set; }//
        public decimal dj { get; set; }//
        public decimal je { get; set; }//
        public decimal gg { get; set; }//
        public decimal ggs { get; set; }//
        public string bz { get; set; }//
        public string khhm1 { get; set; }//
        public string khmc1 { get; set; }//
        public int DB { get; set; }//
        public string dhno { get; set; }
    }
}