using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Creators.Api.V2010.Account;
using Twilio.Readers.Api.V2010.Account;
using Twilio.Fetchers.Api.V2010.Account;
using Twilio.Deleters.Api.V2010.Account;
using Twilio.Types;

namespace ShelterAvailability.Services
{
    public class TwilioSMSService : ISmsSender //, ISmsReceiver
    {

        private readonly SmsSettings _smsSettings;
        private readonly TwilioRestClient _restClient;

        public TwilioSMSService(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
            //TwilioClient.Init(_smsSettings.Sid, _smsSettings.Token);
            _restClient = new TwilioRestClient(_smsSettings.Sid, _smsSettings.Token);

        }

        public async Task SendSmsAsync(string number, string message)
        {
            var messageResource = await
              new MessageCreator(
                _smsSettings.Sid,
                new PhoneNumber(number),  // To number
                new PhoneNumber(_smsSettings.From),  // Twilio From number
                message
              ).ExecuteAsync(_restClient);

            Console.WriteLine(messageResource.GetSid());
        }
    }

    public class SmsSettings
    {
        public string Sid { get; set; }
        public string Token { get; set; }
        public string From { get; set; }
    }
}
