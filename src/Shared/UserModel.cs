﻿namespace Shared
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}