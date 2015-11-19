namespace ChatSystem.Services.Data.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Contracts;
    using Models;
    using TestObjects;
    using System.Linq;

    [TestClass]
    public class PresenceServiceTests
    {
        private IPresenceService presenceService;
        private InMemoryRepository<Presence> presenceRepository;
        private InMemoryRepository<User> userRepository;

        [TestInitialize]
        public void Init()
        {
            this.presenceRepository = TestObjectFactory.GetPresenceRepository();
            this.userRepository = TestObjectFactory.GetUsersRepository();
            this.presenceService = new PresenceService(this.userRepository, this.presenceRepository);
        }

        [TestMethod]
        public void RegisterPresenceShouldReturnCorrectNumberOfSavedChanges()
        {
            var user = new User()
            {
                Id = "112",
                UserName = "Username112",
                FirstName = "User112",
                LastName = "User lastName112"
            };
            this.presenceService.RegisterPresence(user);
            Assert.AreEqual(1, this.presenceRepository.NumberOfSavedChanges);
        }

        [TestMethod]
        public void CheckPresenceServiceShouldReturnTrueWhenValidTimingIsProvided()
        {
            var user = new User()
            {
                Id = "113",
                UserName = "Username113",
                FirstName = "User113",
                LastName = "User lastName113"
            };
            this.userRepository.Add(user);
            this.presenceService.RegisterPresence(user);
            var expected = this.presenceService.CheckPresence("Username113");
            Assert.AreEqual(true, expected);
        }

        [TestMethod]
        public void CheckPresenceShouldReturnFalseWhenUserIsNotPresentMoreThanTimeSpanInterval()
        {
            var testUser = this.presenceRepository.All().FirstOrDefault();
            var newRegisterDate = testUser.RegisteredOn.AddMinutes(-40.0);
            testUser.RegisteredOn = newRegisterDate;
            var expected = this.presenceService.CheckPresence(testUser.User.UserName);
            Assert.AreEqual(false, expected);
        }
    }
}
