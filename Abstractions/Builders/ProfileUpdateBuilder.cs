using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Builders
{
    public class ProfileUpdateBuilder
    {
        private UpdateAddressAndContactsRequest _request;

        public ProfileUpdateBuilder()
        {
            _request = new UpdateAddressAndContactsRequest();
        }

        internal ProfileUpdateBuilder SetServiceConsumer(string serviceConsumer)
        {
            if (string.IsNullOrWhiteSpace(serviceConsumer))
                throw new ArgumentException("Service consumer must be specified");

            _request.ServiceConsumer = serviceConsumer.Trim();

            return this;
        }

        public ProfileUpdateBuilder SetDistributorId(string distributorId)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID must be specified");

            _request.DistributorId = distributorId.Trim();

            return this;
        }

        /// <summary>
        /// Change distributor address
        /// </summary>
        /// <param name="addressType">E.g. SHIP_TO</param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="province"></param>
        /// <param name="zipCode"></param>
        /// <param name="city"></param>
        /// <param name="building"></param>
        /// <param name="addressLines">0-4 lines</param>
        /// <returns></returns>
        public ProfileUpdateBuilder SetAddress(string addressType,
            string country = null,
            string state = null,
            string province = null,
            string zipCode = null,
            string city = null,
            string building = null,
            string[] addressLines = null,
            string careOfName = null)
        {
            if (string.IsNullOrWhiteSpace(addressType))
                throw new ArgumentException("Address type must be specified");

            if (addressLines.Length > 4)
                throw new ArgumentException("Only 4 address lines are allowed");

            if (_request.Address == null)
                _request.Address = new DistributorAddressToUpdate();

            _request.Address.Type = addressType.Trim().ToUpper();

            if (country != null && country.Trim().Length >= 2)
                _request.Address.Country = country.Trim();

            if (state != null)
                _request.Address.State = state.Trim();

            if (province != null)
                _request.Address.Province = province.Trim();

            if (zipCode != null)
                _request.Address.ZipCode = zipCode.Trim();

            if (city != null)
                _request.Address.City = city.Trim();

            if (building != null)
                _request.Address.Building = building.Trim();

            if (addressLines != null && addressLines.Length > 0 && addressLines.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (addressLines.Length > 0 && !string.IsNullOrWhiteSpace(addressLines[0]))
                    _request.Address.Line1 = addressLines[0]?.Trim();

                if (addressLines.Length > 1 && !string.IsNullOrWhiteSpace(addressLines[1]))
                    _request.Address.Line2 = addressLines[1].Trim();

                if (addressLines.Length > 2 && !string.IsNullOrWhiteSpace(addressLines[2]))
                    _request.Address.Line3 = addressLines[2]?.Trim();
                else _request.Address.Line3 = string.Empty;

                if (addressLines.Length > 3 && !string.IsNullOrWhiteSpace(addressLines[3]))
                    _request.Address.Line4 = addressLines[3]?.Trim();
                else _request.Address.Line4 = string.Empty;
            }

            if (careOfName != null)
                _request.Address.CareOfName = careOfName.Trim();

            return this;
        }

        public ProfileUpdateBuilder SetContacts(string type, string value, string subType = null)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Contact type must be specified");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Contact value must be specified");

            if (_request.Contact == null)
                _request.Contact = new DistributorContactToUpdate();

            _request.Contact.Type = type;
            if (!string.IsNullOrWhiteSpace(subType))
                _request.Contact.SubType = subType;
            _request.Contact.Value = value;

            return this;
        }

        internal UpdateAddressAndContactsRequest Build()
        {
            StringBuilder issues = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_request.ServiceConsumer))
                issues.Append("Service consumer must be specified");

            if (string.IsNullOrWhiteSpace(_request.DistributorId))
                issues.Append("Distributor ID must be specified");

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());

            return _request;
        }
    }
}
