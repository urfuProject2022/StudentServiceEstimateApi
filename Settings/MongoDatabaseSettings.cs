﻿namespace StudentEstimateServiceApi.Settings
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollectionName { get; set; }
        public string AuthCollectionName { get; set; }
        public string RoomCollectionName { get; set; }
        public string AssignmentCollectionName { get; set; }
        public string WorkCollectionName { get; set; }
        public string GradeCollectionName { get; set; }
        public string GradeInfoCollectionName { get; set; }
    }
}