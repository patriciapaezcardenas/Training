﻿namespace TestNinja.HousekeeperHelper
{
    public interface IEmailSender
    {
        void EmailFile(string emailAddress, string emailBody, string filename, string subject);
    }
}