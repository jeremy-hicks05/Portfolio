﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAIntranet.Shared
{
    
    [Index(nameof(route_name), Name = "route_name")]
    public partial class MasterRoute
    {
        [NotMapped]
        public List<Pulloff>? Matches
        {
            get
            {
                if (matches is null)
                {
                    matches = new List<Pulloff>();
                }
                return matches;
            }
            set
            {
                matches = value;
            }
        }

        private List<Pulloff>? matches;

        public MasterRoute()
        {

        }

        //public MasterRoute(
        //    int? pk_route_id,
        //    string mode,
        //    string dow,
        //    string route,
        //    string run,
        //    string suffix,
        //    string route_name,
        //    int pYear,
        //    int pMonth,
        //    int pDay,
        //    int pHour,
        //    int pMinute,
        //    int rYear,
        //    int rMonth,
        //    int rDay,
        //    int rHour,
        //    int rMinute,
        //    //DateTime pull_out_time,
        //    //DateTime pull_in_time,
        //    float? beg_dh_miles,
        //    float? end_dh_miles,
        //    List<Pulloff> matches)
        //{
        //    this.PK_Route_ID = pk_route_id;
        //    this.mode = mode;
        //    this.dow = dow;
        //    this.route = route;
        //    this.run = run;
        //    this.suffix = suffix;
        //    this.route_name = route_name;
        //    this.pull_out_time = new DateTime(
        //        pYear, pMonth, pDay, pHour, pMinute, 0);
        //    this.pull_in_time = new DateTime(
        //        rYear, rMonth, rDay, rHour, rMinute, 0
        //        );

        //    if (this.pull_in_time < this.pull_out_time)
        //    {
        //        this.pull_in_time = new DateTime(
        //            this.pull_in_time.Value.AddDays(1).Year,
        //            this.pull_in_time.Value.AddDays(1).Month,
        //            this.pull_in_time.Value.AddDays(1).Day,
        //            this.pull_in_time.Value.Hour,
        //            this.pull_in_time.Value.Minute,
        //            0);
        //    }

        //    this.beg_dh_miles = beg_dh_miles;
        //    this.end_dh_miles = end_dh_miles;
        //    this.Matches = matches;
        //}

        public MasterRoute(MasterRoute masterRoute, DateTime date)
        {
            this.PK_Route_ID = masterRoute.PK_Route_ID;
            this.mode = masterRoute.mode;
            this.dow = masterRoute.dow;
            this.route = masterRoute.route;
            this.run = masterRoute.run;
            this.suffix = masterRoute.suffix;
            this.route_name = masterRoute.route_name;
            this.pull_out_time = new DateTime(
                                            date.Year,
                                            date.Month,
                                            date.Day,
                                            masterRoute.pull_out_time!.Value.Hour,
                                            masterRoute.pull_out_time.Value.Minute,
                                            0);
            this.pull_in_time = new DateTime(
                                            date.Year,
                                            date.Month,
                                            date.Day,
                                            masterRoute.pull_in_time!.Value.Hour,
                                            masterRoute.pull_in_time.Value.Minute,
                                            0);

            if (this.pull_in_time < this.pull_out_time)
            {
                this.pull_in_time = new DateTime(
                    this.pull_in_time.Value.AddDays(1).Year,
                    this.pull_in_time.Value.AddDays(1).Month,
                    this.pull_in_time.Value.AddDays(1).Day,
                    this.pull_in_time.Value.Hour,
                    this.pull_in_time.Value.Minute,
                    0);
            }

            this.beg_dh_miles = masterRoute.beg_dh_miles;
            this.end_dh_miles = masterRoute.end_dh_miles;
            this.matches = null;
        }

        [Key]
        public int? PK_Route_ID { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [StringLength(255)]
        public string? mode { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [RegularExpression("^[mtwhfsyMTWHFSY]$", 
            ErrorMessage = "Please enter M, T, W, H, F, S, or Y")]
        [StringLength(255)]
        public string? dow { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [StringLength(255)]
        public string? route { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [StringLength(255)]
        public string? run { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [StringLength(255)]
        public string? suffix { get; set; }

        [Column(TypeName = "nvarchar (255)")]
        [StringLength(255)]
        public string? route_name { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? pull_out_time { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? pull_in_time { get; set; }

        [Column(TypeName = "float")]
        public float? beg_dh_miles { get; set; }

        [Column(TypeName = "float")]
        public float? end_dh_miles { get; set; }

        public string GetSignature()
        {
            return (route_name + mode + dow + route + run + suffix).Replace(" ", "");
        }
        public string GetUniqueSignature()
        {
            return (route_name + mode + route + run + suffix + pull_out_time!.Value.Month + pull_out_time.Value.Day).Replace(" ", "");
        }
    }
}
