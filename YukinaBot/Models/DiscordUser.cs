﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YukinaBot.Models
{
    public class DiscordUser
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string name { get; set; }
        [BsonElement]
        public ulong discordId { get; set; }
        [BsonElement]
        public string anilistName { get; set; }
        [BsonElement]
        public long anilistId { get; set; }
    }
}
