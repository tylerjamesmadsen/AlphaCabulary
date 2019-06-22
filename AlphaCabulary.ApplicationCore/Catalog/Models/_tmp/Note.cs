using System;
using SQLite;

namespace AlphaCabulary.ApplicationCore.Catalog.Models._tmp
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}