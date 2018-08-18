using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema
{
    public static class Settings
    {
        public enum SelectionMode
        {
            Countries,
            Genres,
            Films
        }
        public static string ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=Cinema;Integrated Security=True";
        public static int? UserID = null;
        public static string UserName = null;
        public static bool Editing = false;
        public static bool Booking = false;
    }
}
