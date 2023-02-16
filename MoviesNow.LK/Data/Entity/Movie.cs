namespace MoviesNow.LK.Data.Entity
{
    public class Movie : Base
    {
        public string movieName { get; set; }
        public string movieUrl { get; set; }
        public string posterUrl { get; set; }
        public string trailerUrl { get; set; }
        public string imageUrl { get; set; }
        public string directDownUrl { get; set; }
        public string magnetDownUrl { get; set; }
        public string torrentDownUrl { get; set; }
        public string subDownUrl { get; set; }
        public string alterSubDownUrl { get; set; }
        public string screenshotUrl { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public DateOnly releaseDate { get; set; }
        public DateOnly addedDate { get; set; }
        public int rating { get; set; }
        public string language { get; set; }
        public string audio { get; set; }
        public string genre { get; set; }
        public string classification { get; set; }
        public string quality { get; set; }
        public double runTime { get; set; }
        public string author { get; set; }
        public bool IsActive { get; set; }
    }
}
