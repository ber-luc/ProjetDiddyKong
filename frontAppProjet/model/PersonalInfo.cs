using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace clicker.model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Score { get; set; }
        public int ClickPower { get; set; }
        public int TimerPower { get; set; }
        public int Playtime { get; set; }
        public string Token { get; set; }
    }
}
