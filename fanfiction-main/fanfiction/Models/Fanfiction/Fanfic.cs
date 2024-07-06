﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using fanfiction.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Runtime.CompilerServices;
using fanfiction.Data;

namespace fanfiction.Models.Fanfiction
{
    public class Fanfic
    {
        [Key]
        public int FanficId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lang { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
        
        [Required]
        public Genre genre { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public Fandom fandom { get; set; }
        
        public List<Chapter> Chapters { get; set; }
        
        [Required]
        public int FandomId { get; set; }
        public List<Tag> Tags { get; set; }

        public Fanfic()
        {
            Author = new ApplicationUser();
            fandom = new Fandom();
            genre = new Genre();
        }
        public Fanfic(AddFanficData data, ApplicationDbContext context, string lang, ApplicationUser author)
        {
            Description = data.Description;
            Author = author;
            Name = data.name;
            Lang = data.lang;
            fandom = context.GetFandom(data.fandomName, lang);
            genre = context.GetGenre(data.genreName, lang);
            
        }
    }

    public class FanficModel
    {
        public Rate urRate  {get; set;}
        public RateModel rateModel { get; set; }
        public Comment urComment {get; set;}
        public List<Comment> Comments {get;}
        public bool isMine;
        public Fanfic fanfic { get; set; }
        public string lang { get; }
        public bool IsSignedIn;

        public FanficModel()
        {
            fanfic = new Fanfic();
            rateModel = new RateModel();
        }

        public FanficModel(Fanfic fanfic, string lang, string Id, List<Comment> comments, List<Rate> rates, bool isAdmin)
        {
            rateModel = new RateModel(rates, Id);
            urComment = new Comment();
            this.Comments = comments;
            this.fanfic = fanfic;
            this.lang = lang;
            if (isAdmin)
            {
                isMine = true;
                IsSignedIn = true;
            }
            else
            {
                if (Id == string.Empty)
                {
                    isMine = false;
                    IsSignedIn = false;
                }
                else
                {
                    isMine = fanfic.ApplicationUserId == Id;
                    IsSignedIn = true;
                }     
            }
            
           
            
        }
        
    }
    public class FanfictionModel
    {
        public List<FanficModel> fanfiction { get; }
        public string lang { get; }

        public FanfictionModel()
        {
            fanfiction = new List<FanficModel>();
        }

        public FanfictionModel(ApplicationDbContext context, string lang)
        {
            
            fanfiction = new List<FanficModel>();
            var list = context.Fanfics.ToList();
            var rates = context.Rates.ToList();
            foreach (var l in list)
            {
                var model = new RateModel();
                model.Rates = rates.Where(r => r.fanficId == l.FanficId).ToList();
                if(model.Rates.Count != 0) model.average = model.Rates.Average(r => r.rate);

                    fanfiction.Add(new FanficModel {
                    fanfic =  l, rateModel = model                    
                });
            }

            for (int i = 0; i < fanfiction.Count; i++)
            {
                fanfiction[i].fanfic = context.GetAllFanficData(fanfiction[i].fanfic);
            }
            this.lang = lang;
        }
        public FanfictionModel(ApplicationDbContext context, string lang, string userId)
        {
            
            fanfiction = new List<FanficModel>();
            var list = context.Fanfics.Where(f => f.ApplicationUserId == userId).ToList();
            var rates = context.Rates.ToList();
            foreach (var l in list)
            {
                var model = new RateModel();
                model.Rates = rates.Where(r => r.fanficId == l.FanficId).ToList();
                if(model.Rates.Count != 0) model.average = model.Rates.Average(r => r.rate);

                fanfiction.Add(new FanficModel {
                    fanfic =  l, rateModel = model                    
                });
            }

            for (int i = 0; i < fanfiction.Count; i++)
            {
                fanfiction[i].fanfic = context.GetAllFanficData(fanfiction[i].fanfic);
            }
            this.lang = lang;
        }

    }

}
