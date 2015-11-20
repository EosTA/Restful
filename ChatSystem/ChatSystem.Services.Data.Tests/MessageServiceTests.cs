namespace ChatSystem.Services.Data.Tests
{
    using System.Linq;

    using ChatSystem.Data.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services.Data.Contracts;

    using TestObjects;

    [TestClass]
    public class MessageServiceTests
    {
        private IMessagesService messageService;

        private InMemoryRepository<ChatMessage> messageRepository;
        private InMemoryRepository<User> userRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.messageRepository = RepositoriesTestObjectFactory.GetMessageRepository();
            this.userRepository = RepositoriesTestObjectFactory.GetUsersRepository();
            this.messageService = new MessagesService(this.messageRepository, this.userRepository);
        }

        [TestMethod]
        public void AddShouldReturnCorrentNumberOfSavedChanges()
        {
            this.messageService.Add("Message", "User1", "User5");
            var actual = this.messageRepository.NumberOfSavedChanges;
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void AddShouldInsertCorrectValuesInRepository()
        {
            this.messageService.Add("Another message", "Username1", "Username8");
            var actual = this.messageRepository.All().FirstOrDefault(ms => ms.Message.Contains("Another"));

            Assert.IsNotNull(actual);
            Assert.AreEqual("Username1", actual.Sender.UserName);
            Assert.AreEqual("Another message", actual.Message);
            Assert.AreEqual("Username8", actual.Receiver.UserName);
        }

        [TestMethod]
        public void AllShouldReturnCorrectCountOfMessages()
        {
            var currentCount = this.messageRepository.All().Count();
            Assert.AreEqual(20, currentCount);

            this.messageService.Add("Message", "Username3", "Username5");
            this.messageService.Add("Message", "Username5", "Username6");

            var actual = this.messageRepository.All().Count();
            Assert.AreEqual(22, actual);
        }

        [TestMethod]
        public void RepositoryDisposeShouldReturnTrueWhenDisposed()
        {
            this.messageRepository.Dispose();
            var actual = this.messageRepository.IsDisposed;
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void RepositoryGetShouldReturnNotNullableValue()
        {
            // id is fake! should return the first value in the dummy base.
            var actual = this.messageRepository.GetById("1");
            Assert.IsNotNull(actual);
            Assert.AreEqual("User0", actual.Sender.UserName);
            Assert.AreEqual("User01", actual.Receiver.UserName);
        }
    }
}
