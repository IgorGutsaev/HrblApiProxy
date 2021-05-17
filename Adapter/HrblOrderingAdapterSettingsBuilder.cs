using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapterSettingsBuilder
    {
        private HrblOrderingAdapterSettings _adapterSettings = new HrblOrderingAdapterSettings();

        /// <summary>
        /// Add service url
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public HrblOrderingAdapterSettingsBuilder WithUri(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("Uri is mandatory");

            _adapterSettings.ApiUri = uri;

            return this;
        }

        /// <summary>
        /// Add service consumer
        /// </summary>
        /// <param name="consumer">Service consumer</param>
        /// <example>AAKIOSK</example>
        /// <returns></returns>
        public HrblOrderingAdapterSettingsBuilder WithServiceConsumer(string consumer)
        {
            if (string.IsNullOrWhiteSpace(consumer))
                throw new ArgumentException("Consumer is mandatory");

            _adapterSettings.Consumer = consumer;

            return this;
        }

        /// <summary>
        /// Add service consumer
        /// </summary>
        /// <param name="consumer">Service consumer</param>
        /// <example>AAKIOSK</example>
        /// <returns></returns>
        public HrblOrderingAdapterSettingsBuilder WithOrganizationId(uint organizationId)
        {
            if (organizationId == 0)
                throw new ArgumentException("Organization Id is mandatory");

            _adapterSettings.OrganizationId = organizationId;

            return this;
        }

        /// <summary>
        /// Add service consumer
        /// </summary>
        /// <param name="consumer">Service consumer</param>
        /// <example>AAKIOSK</example>
        /// <returns></returns>
        public HrblOrderingAdapterSettingsBuilder WithCredentials(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Credentials are invalid");

            _adapterSettings.Login = login;
            _adapterSettings.Password = password;

            return this;
        }        
        
        public HrblOrderingAdapterSettings Build() => _adapterSettings;
    }
}
