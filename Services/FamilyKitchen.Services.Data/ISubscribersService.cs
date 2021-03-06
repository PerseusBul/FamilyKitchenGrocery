﻿namespace FamilyKitchen.Services.Data
{
    using System.Threading.Tasks;

    public interface ISubscribersService
    {
        Task<bool> SubscribeEmail(string username, string subscriber);

        string SendEmail(string name, string email, string message);
    }
}
