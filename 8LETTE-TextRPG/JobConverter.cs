using _8LETTE_TextRPG.ItemFolder;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8LETTE_TextRPG.JobFolder;

namespace _8LETTE_TextRPG
{
    class JobConverter : JsonConverter<JobBase>
    {
        public override JobBase? ReadJson(JsonReader reader, Type objectType, JobBase? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject jo = JObject.Load(reader);

            JobType jobType = jo["JobType"]?.ToObject<JobType>() ?? throw new NullReferenceException();
            PromotionType promotionType = jo["PromotionType"]?.ToObject<PromotionType>() ?? throw new NullReferenceException();

            JobBase? job;
            switch (jobType)
            {
                case JobType.Junior:
                    job = new Junior();
                    break;
                case JobType.BugWarrior:
                    job = new BugWarrior(promotionType);
                    break;
                case JobType.MemoryKnight:
                    job = new MemoryKnight(promotionType);
                    break;
                case JobType.ThreadAssassin:
                    job = new ThreadAssassin(promotionType);
                    break;
                case JobType.ExceptionHunter:
                    job = new ExceptionHunter(promotionType);
                    break;
                default:
                    throw new JsonSerializationException($"Unknown ItemType: {jobType}");
            }

            return job;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, JobBase? value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // WriteJson은 호출되면 안됨
        }
    }
}
