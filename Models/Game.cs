﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace MediaManager.Models
{
    public class Game
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }

        [Display(Name = "On Loan")]
        public string OnLoan { get; set; }

        [Display(Name = "Loaned To")]
        public int LoanedToID { get; set; }

        [Display(Name = "Date Loaned")]
        public DateTime LoanedDate { get; set; }

        [Display(Name = "Loan Length")]
        public string LoanLength
        {
            get
            {
                string loanedLength = String.Empty;

                using (GameDBContext db = new GameDBContext())
                {
                    DateTime? loanDate = (from games in db.Games
                                          where games.ID == ID
                                          select LoanedDate).SingleOrDefault();

                    if (loanDate.HasValue)
                    {
                        TimeSpan elapsed = DateTime.Now.Subtract(loanDate.Value);
                        loanedLength = elapsed.TotalDays.ToString("0") + " days";
                    }
                }

                return loanedLength;
            }
        }
    }

    public class GameDBContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}