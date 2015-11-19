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

        [TestMethod]
        public void UpdatePresenceShouldRegisterPresenceIfNotRegisteredInTimeInterval()
        {
            var user = new User()
            {
                Id = "118",
                UserName = "Username118",
                FirstName = "User118",
                LastName = "User lastName118"
            };
            this.userRepository.Add(user);
            this.presenceService.UpdatePresence(user.UserName);
            var count = this.presenceRepository.All().ToList().Count;
            Assert.AreEqual(21,count);
        }

        [TestMethod]
        public void UpdatePresenceShouldNotRegisterPresenceIfUserIsRegisteredInTimeInterval()
        {
            var userToUpdatePresence = this.userRepository.All().FirstOrDefault();
            var userWithPresence = this.presenceRepository.All().Where(u =>
                                                u.UserId == userToUpdatePresence.Id).FirstOrDefault();
            var newTimeSpan = userWithPresence.RegisteredOn.AddMinutes(-30.0);
            userWithPresence.RegisteredOn = newTimeSpan;
            this.presenceService.UpdatePresence(userWithPresence.User.UserName);
            var count = this.presenceRepository.All().ToList().Count;
            Assert.AreEqual(20, count);
        }
    }
}
