﻿namespace ToDoListify.API.Models.DTO
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
