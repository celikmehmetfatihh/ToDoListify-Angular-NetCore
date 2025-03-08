﻿namespace ToDoListify.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        // Navigation property
        public ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
