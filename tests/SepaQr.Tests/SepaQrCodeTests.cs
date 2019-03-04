using System;
using FluentAssertions;
using Xunit;

namespace SepaQr.Tests
{
    public class SepaQrCodeTests
    {
        [Fact]
        public void Construct_ServiceTagShouldBeBCD()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.ServiceTag.Should().Be("BCD");
        }

        [Fact]
        public void Construct_VersionShouldBeTwo()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Version.Should().Be(2);
        }

        [Fact]
        public void Construct_CharacterSetShouldBeUtf8()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.CharacterSet.Should().Be(CharacterSet.UTF8);
        }

        [Fact]
        public void Construct_IdentificationCodeShouldBeSCT()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.IdentificationCode.Should().Be("SCT");
        }

        [Fact]
        public void Construct_BicShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Bic.Should().BeNull();
        }

        [Fact]
        public void Construct_NameShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Name.Should().BeNull();
        }

        [Fact]
        public void Construct_AccountNumberShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.AccountNumber.Should().BeNull();
        }

        [Fact]
        public void Construct_AmountShouldBeZero()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Amount.Should().Be(0);
        }

        [Fact]
        public void Construct_PurposeShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Purpose.Should().BeNull();
        }


        [Fact]
        public void Construct_StructuredRemittanceInformationShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.StructuredRemittanceInformation.Should().BeNull();
        }

        [Fact]
        public void Construct_UnstructuredRemittanceInformationShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.UnstructuredRemittanceInformation.Should().BeNull();
        }

        [Fact]
        public void Construct_InformationShouldBeNull()
        {
            // Act
            var code = new SepaQrCode();

            // Assert
            code.Information.Should().BeNull();
        }

        [Theory]
        [InlineData(-10, true)]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(10, true)]
        public void SetVersion_ShouldSetVersionOrThrowExceptionOnInvalidValue(int version, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetVersion(version);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("Only 1 or 2 is allowed*")
                    .And.ParamName.Should().Be("version");
            }
            else
            {
                action.Should().NotThrow();
                code.Version.Should().Be(version);
            }
        }

        [Fact]
        public void SetBic_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetBic(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("bic");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("12345678", false)]
        [InlineData(" 12345678  ", false)]
        [InlineData("12345678901", false)]
        [InlineData(" 12345678901  ", false)]
        [InlineData("123456789012", true)]
        public void SetBic_ShouldSetBicOrThrowExceptionOnInvalidValue(string bic, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetBic(bic);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length of 8 or 11*")
                    .And.ParamName.Should().Be("bic");
            }
            else
            {
                action.Should().NotThrow();
                code.Bic.Should().Be(bic.Trim());
            }
        }

        [Fact]
        public void ClearBic_ShouldSetBicToNull()
        {
            // Arrange
            var code = new SepaQrCode();
            code.SetBic("12345678");
            code.Bic.Should().NotBeNull();

            // Act
            code.ClearBic();

            // Assert
            code.Bic.Should().BeNull();
        }

        [Fact]
        public void SetName_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetName(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("name");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890", false)]
        [InlineData(" 1234567890123456789012345678901234567890123456789012345678901234567890 ", false)]
        [InlineData(" 12345678901234567890123456789012345678901234567890123456789012345678901 ", true)]
        public void SetName_ShouldSetNameOrThrowExceptionOnInvalidLength(string name, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetName(name);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 70*")
                    .And.ParamName.Should().Be("name");
            }
            else
            {
                action.Should().NotThrow();
                code.Name.Should().Be(name.Trim());
            }
        }

        [Fact]
        public void SetAccountNumber_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetAccountNumber(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("accountNumber");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("1234567890123456789012345678901234", false)]
        [InlineData(" 1234567890123456789012345678901234 ", false)]
        [InlineData(" 12345678901234567890123456789012345 ", true)]
        public void SetAccountNumber_ShouldSetAccountNumberOrThrowExceptionOnInvalidLength(string accountNumber, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetAccountNumber(accountNumber);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 34*")
                    .And.ParamName.Should().Be("accountNumber");
            }
            else
            {
                action.Should().NotThrow();
                code.AccountNumber.Should().Be(accountNumber.Trim());
            }
        }

        [Theory]
        [InlineData(-10, true)]
        [InlineData(0, true)]
        [InlineData(0.005, true)]
        [InlineData(0.01, false)]
        [InlineData(100, false)]
        [InlineData(999999999.99, false)]
        [InlineData(1000000000, true)]
        public void SetAmount_ShouldSetAmountOrThrowExceptionOnInvalidValue(decimal amount, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetAmount(amount);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should be between 0.01 and 999999999.99*")
                    .And.ParamName.Should().Be("amount");
            }
            else
            {
                action.Should().NotThrow();
                code.Amount.Should().Be(amount);
            }
        }

        [Fact]
        public void SetPurpose_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetPurpose(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("purpose");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("1234", false)]
        [InlineData(" 1234 ", false)]
        [InlineData(" 12345 ", true)]
        public void SetPurpose_ShouldSetPurposeOrThrowExceptionOnInvalidLength(string purpose, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetPurpose(purpose);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 4*")
                    .And.ParamName.Should().Be("purpose");
            }
            else
            {
                action.Should().NotThrow();
                code.Purpose.Should().Be(purpose.Trim());
            }
        }

        [Fact]
        public void ClearPurpose_ShouldSetPurposeToNull()
        {
            // Arrange
            var code = new SepaQrCode();
            code.SetPurpose("1234");
            code.Purpose.Should().NotBeNull();

            // Act
            code.ClearPurpose();

            // Assert
            code.Purpose.Should().BeNull();
        }

        [Fact]
        public void SetStructuredRemittanceInformation_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetStructuredRemittanceInformation(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("structuredRemittanceInformation");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("12345678901234567890123456789012345", false)]
        [InlineData(" 12345678901234567890123456789012345 ", false)]
        [InlineData(" 123456789012345678901234567890123456 ", true)]
        public void SetStructuredRemittanceInformation_ShouldSetStructuredRemittanceInformationOrThrowExceptionOnInvalidLength(string structuredRemittanceInformation, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetStructuredRemittanceInformation(structuredRemittanceInformation);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 35*")
                    .And.ParamName.Should().Be("structuredRemittanceInformation");
            }
            else
            {
                action.Should().NotThrow();
                code.StructuredRemittanceInformation.Should().Be(structuredRemittanceInformation.Trim());
            }
        }

        [Fact]
        public void ClearStructuredRemittanceInformation_ShouldSetStructuredRemittanceInformationToNull()
        {
            // Arrange
            var code = new SepaQrCode();
            code.SetStructuredRemittanceInformation("1234");
            code.StructuredRemittanceInformation.Should().NotBeNull();

            // Act
            code.ClearStructuredRemittanceInformation();

            // Assert
            code.StructuredRemittanceInformation.Should().BeNull();
        }

        [Fact]
        public void SetUnstructuredRemittanceInformation_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetUnstructuredRemittanceInformation(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("unstructuredRemittanceInformation");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", false)]
        [InlineData(" 12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890 ", false)]
        [InlineData(" 123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901 ", true)]
        public void SetUnstructuredRemittanceInformation_ShouldSetUnstructuredRemittanceInformationOrThrowExceptionOnInvalidLength(string unstructuredRemittanceInformation, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetUnstructuredRemittanceInformation(unstructuredRemittanceInformation);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 140*")
                    .And.ParamName.Should().Be("unstructuredRemittanceInformation");
            }
            else
            {
                action.Should().NotThrow();
                code.UnstructuredRemittanceInformation.Should().Be(unstructuredRemittanceInformation.Trim());
            }
        }

        [Fact]
        public void ClearUnstructuredRemittanceInformation_ShouldSetUnstructuredRemittanceInformationToNull()
        {
            // Arrange
            var code = new SepaQrCode();
            code.SetUnstructuredRemittanceInformation("1234");
            code.UnstructuredRemittanceInformation.Should().NotBeNull();

            // Act
            code.ClearUnstructuredRemittanceInformation();

            // Assert
            code.UnstructuredRemittanceInformation.Should().BeNull();
        }

        [Fact]
        public void SetInformation_PassNull_ShouldThrowException()
        {
            // Arrange
            var code = new SepaQrCode();

            // Act
            Action action = () => code.SetInformation(null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("information");
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("   ", true)]
        [InlineData("A", false)]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890", false)]
        [InlineData(" 1234567890123456789012345678901234567890123456789012345678901234567890 ", false)]
        [InlineData(" 12345678901234567890123456789012345678901234567890123456789012345678901 ", true)]
        public void SetInformation_ShouldSetInformationOrThrowExceptionOnInvalidLength(string information, bool shouldThrow)
        {
            // Arrange
            var code = new SepaQrCode();

            // Action
            Action action = () => code.SetInformation(information);

            // Assert
            if (shouldThrow)
            {
                action.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("The value should have a length between 1 and 70*")
                    .And.ParamName.Should().Be("information");
            }
            else
            {
                action.Should().NotThrow();
                code.Information.Should().Be(information.Trim());
            }
        }

        [Fact]
        public void ClearInformation_ShouldSetInformationToNull()
        {
            // Arrange
            var code = new SepaQrCode();
            code.SetInformation("1234");
            code.Information.Should().NotBeNull();

            // Act
            code.ClearInformation();

            // Assert
            code.Information.Should().BeNull();
        }
    }
}
