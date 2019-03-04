using System;
using System.Globalization;
using System.Text;

namespace SepaQr
{
    public sealed class SepaQrCode
    {
        /// <summary>
        /// Gets the service tag.
        /// </summary>
        public readonly string ServiceTag = SepaQrCodeDefaults.DefaultServiceTag;

        /// <summary>
        /// Gets the version.
        /// </summary>
        public int Version { get; private set; } = SepaQrCodeDefaults.DefaultVersion;

        /// <summary>
        /// Gets the character set.
        /// </summary>
        public readonly CharacterSet CharacterSet = SepaQrCodeDefaults.DefaultCharacterSet;

        /// <summary>
        /// Gets the identification code.
        /// </summary>
        public readonly string IdentificationCode = SepaQrCodeDefaults.DefaultIdentificationCode;

        /// <summary>
        /// Gets the BIC of the beneficiary bank.
        /// </summary>
        public string Bic { get; private set; }

        /// <summary>
        /// Gets the name of the beneficiary.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the account number of the beneficiary. Only IBAN is allowed.
        /// </summary>
        public string AccountNumber { get; private set; }

        /// <summary>
        /// Gets the amount of credit transfer in EURO.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the purpose of the credit transfer.
        /// </summary>
        public string Purpose { get; private set; }

        /// <summary>
        /// Gets the structured remittance information.
        /// </summary>
        public string StructuredRemittanceInformation { get; private set; }

        /// <summary>
        /// Gets the unstructured remittance information.
        /// </summary>
        public string UnstructuredRemittanceInformation { get; private set; }

        /// <summary>
        /// Gets the beneficiary to originator information.
        /// </summary>
        public string Information { get; private set; }

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="version">The version. Only 1 or 2 is allowed.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetVersion(int version)
        {
            if (version < 1 || version > 2)
                throw new ArgumentOutOfRangeException(nameof(version), "Only 1 or 2 is allowed");
            Version = version;
            return this;
        }

        /// <summary>
        /// Sets the BIC of the beneficiary bank.
        /// </summary>
        /// <param name="bic">The BIC of the beneficiary bank (should be null or contain 8 or 11 characters).</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetBic(string bic)
        {
            bic = bic?.Trim() ?? throw new ArgumentNullException(nameof(bic));
            if (bic.Length != 8 && bic.Length != 11)
                throw new ArgumentOutOfRangeException(nameof(bic), "The value should have a length of 8 or 11");
            Bic = bic;
            return this;
        }

        /// <summary>
        /// Clears the BIC of the beneficiary bank.
        /// </summary>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode ClearBic()
        {
            Bic = null;
            return this;
        }

        /// <summary>
        /// Sets the name of the beneficiary.
        /// </summary>
        /// <param name="name">The name of the beneficiary.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetName(string name)
        {
            name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            if (name.Length < 1 || name.Length > 70)
                throw new ArgumentOutOfRangeException(nameof(name), "The value should have a length between 1 and 70");
            Name = name;
            return this;
        }

        /// <summary>
        /// Sets the account number of the beneficiary.
        /// </summary>
        /// <param name="accountNumber">The account number of the beneficiary. Only IBAN is allowed.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetAccountNumber(string accountNumber)
        {
            accountNumber = accountNumber?.Trim() ?? throw new ArgumentNullException(nameof(accountNumber));
            if (accountNumber.Length < 1 || accountNumber.Length > 34)
                throw new ArgumentOutOfRangeException(nameof(accountNumber), "The value should have a length between 1 and 34");
            AccountNumber = accountNumber;
            return this;
        }

        /// <summary>
        /// Sets the amount of credit transfer in EURO.
        /// </summary>
        /// <param name="amount">The amount of credit transfer in EURO</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetAmount(decimal amount)
        {
            if (amount < 0.01M || amount > 999999999.99M)
                throw new ArgumentOutOfRangeException(nameof(amount), "The value should be between 0.01 and 999999999.99");
            Amount = amount;
            return this;
        }

        /// <summary>
        /// Sets the purpose of the credit transfer.
        /// </summary>
        /// <param name="purpose">The purpose of the credit transfer.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetPurpose(string purpose)
        {
            purpose = purpose?.Trim() ?? throw new ArgumentNullException(nameof(purpose));
            if (purpose.Length < 1 || purpose.Length > 4)
                throw new ArgumentOutOfRangeException(nameof(purpose), "The value should have a length between 1 and 4");
            Purpose = purpose;
            return this;
        }

        /// <summary>
        /// Clears the purpose of the credit transfer.
        /// </summary>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode ClearPurpose()
        {
            Purpose = null;
            return this;
        }

        /// <summary>
        /// Sets the structured remittance information.
        /// </summary>
        /// <param name="structuredRemittanceInformation">The structured remittance information.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetStructuredRemittanceInformation(string structuredRemittanceInformation)
        {
            structuredRemittanceInformation = structuredRemittanceInformation?.Trim() ?? throw new ArgumentNullException(nameof(structuredRemittanceInformation));
            if (structuredRemittanceInformation.Length < 1 || structuredRemittanceInformation.Length > 35)
                throw new ArgumentOutOfRangeException(nameof(structuredRemittanceInformation), "The value should have a length between 1 and 35");
            StructuredRemittanceInformation = structuredRemittanceInformation;
            return this;
        }

        /// <summary>
        /// Clears the structured remittance information.
        /// </summary>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode ClearStructuredRemittanceInformation()
        {
            StructuredRemittanceInformation = null;
            return this;
        }

        /// <summary>
        /// Sets the unstructured remittance information.
        /// </summary>
        /// <param name="unstructuredRemittanceInformation">The unstructured remittance information.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetUnstructuredRemittanceInformation(string unstructuredRemittanceInformation)
        {
            unstructuredRemittanceInformation = unstructuredRemittanceInformation?.Trim() ?? throw new ArgumentNullException(nameof(unstructuredRemittanceInformation));
            if (unstructuredRemittanceInformation.Length < 1 || unstructuredRemittanceInformation.Length > 140)
                throw new ArgumentOutOfRangeException(nameof(unstructuredRemittanceInformation), "The value should have a length between 1 and 140");
            UnstructuredRemittanceInformation = unstructuredRemittanceInformation;
            return this;
        }

        /// <summary>
        /// Clears the unstructured remittance information.
        /// </summary>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode ClearUnstructuredRemittanceInformation()
        {
            UnstructuredRemittanceInformation = null;
            return this;
        }

        /// <summary>
        /// Sets the beneficiary to originator information.
        /// </summary>
        /// <param name="information">The beneficiary to originator information.</param>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode SetInformation(string information)
        {
            information = information?.Trim() ?? throw new ArgumentNullException(nameof(information));
            if (information.Length < 1 || information.Length > 70)
                throw new ArgumentOutOfRangeException(nameof(information), "The value should have a length between 1 and 70");
            Information = information;
            return this;
        }

        /// <summary>
        /// Clears the beneficiary to originator information.
        /// </summary>
        /// <returns>The <see cref="SepaQrCode"/> instance.</returns>
        public SepaQrCode ClearInformation()
        {
            Information = null;
            return this;
        }

        /// <summary>
        /// Gets the QR content.
        /// </summary>
        /// <returns>The QR content.</returns>
        public string GetQrContent()
        {
            if (Version == 1 && Bic == null)
                throw new InvalidOperationException("BIC is required for version 1");

            if (Name == null)
                throw new InvalidOperationException($"{nameof(Name)} is required");

            if (AccountNumber == null)
                throw new InvalidOperationException($"{nameof(AccountNumber)} is required");

            if (Amount == 0)
                throw new InvalidOperationException($"{nameof(Amount)} is requried");

            if (StructuredRemittanceInformation != null && UnstructuredRemittanceInformation != null)
                throw new InvalidOperationException($"Only {nameof(StructuredRemittanceInformation)} or {nameof(UnstructuredRemittanceInformation)} can be set, not both");

            var content = new StringBuilder();
            content.AppendLine(ServiceTag);
            content.AppendLine($"{Version:D3}");
            content.AppendLine($"{(char)CharacterSet}");
            content.AppendLine(IdentificationCode);
            content.AppendLine(Bic ?? string.Empty);
            content.AppendLine(Name);
            content.AppendLine(AccountNumber);
            content.AppendLine($"EUR{Amount.ToString("0.00", CultureInfo.InvariantCulture)}");
            content.AppendLine(Purpose ?? string.Empty);
            content.AppendLine(StructuredRemittanceInformation ?? string.Empty);
            content.AppendLine(UnstructuredRemittanceInformation ?? string.Empty);
            content.AppendLine(Information ?? string.Empty);
            return content.ToString().Replace(Environment.NewLine, "\n");
        }
    }
}
