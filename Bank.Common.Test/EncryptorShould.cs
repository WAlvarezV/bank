using Bank.Common.Security;

namespace Bank.Common.Test
{
    public class EncryptorShould
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Is_Null()
        {
            //Arrange
            string password = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => Encryptor.Encrypt(password));
        }

        [Fact]
        public void Throw_ArgumentException_When_Is_Empty()
        {
            //Arrange
            string password = string.Empty;
            //Act & Assert
            Assert.Throws<ArgumentException>(() => Encryptor.Encrypt(password));
        }

        [Fact]
        public void Return_Base64String_When_Receives_A_String()
        {
            //Arrange
            string password = "2db$%ewWE";
            //Act 
            var encrypted = Encryptor.Encrypt(password);
            // Assert
            Assert.False(password.Equals(encrypted));
        }
    }
}