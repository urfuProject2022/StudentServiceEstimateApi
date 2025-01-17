﻿using System.Collections.Generic;
using MongoDB.Bson;

namespace StudentEstimateServiceApi.Models.DTO
{
    public class WorkDto
    {
        public ObjectId WorkId { get; set; }
        public string TextAnswer { get; set; }
        public List<FileDto> FileAnswers { get; set; }
    }
}
