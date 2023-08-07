﻿namespace RouteMasterFrontend.Models.Dto
{
    public class Comments_AccommodationIndexDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }

        public string HotelName { get; set; }

        public int Score { get; set; }

        public string? Title { get; set; }

        public string? Pros { get; set; }

        public string? Cons { get; set; }

        public DateTime CreateDate { get; set; }

        public string Status { get; set; }

        public string? ReplyMessage { get; set; }

        public DateTime? ReplyDate { get; set; }

        public IEnumerable<string>? ImageList { get; set; }

        public bool ThumbsUp { get; set; }   

    }
}