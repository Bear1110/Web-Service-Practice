﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    public class Room
    {
        public long Id { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }
    }
}