using NUnit.Framework;
using fanfiction;
using fanfiction.Controllers;
using fanfiction.Models.User;
using fanfiction.Models.Fanfiction;

namespace FanfictionTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TEmailCorrect()
        {
            string a = "abcdefgh4@mail.com";
            UserReg userReg = new UserReg();
            userReg.Email = a;
            int expected = 18;
            Assert.AreEqual(expected, a.Length);
        }
        [Test]
        public void TEmailIncorrect()
        {
            string a = "abcdefghbiliboba@mail.com";
            UserReg userReg = new UserReg();
            userReg.Email = a;
            int expected = 20;
            Assert.AreNotEqual(expected, userReg.Email.Length);
        }
        [Test]
        public void TEmailIncorrectByAnDogsign()
        {
            string a = "abcdefghbilibobacom";
            UserReg userReg = new UserReg();
            userReg.Email = a;
            bool expected = true;
            Assert.AreNotEqual(expected, a.Contains('@'));
        }
        
        [Test]
        public void TPassword()
        {
            string a = "uyt4jfcjfg";
            UserReg userReg = new UserReg();
            userReg.Password = a;
            Assert.True(a.Length > 8 && a.Length < 20);
          
        }        
        [Test]
        public void TFandomEn()
        {
            string name = "gbjobrrghuorbgouhrbgouhbgohubgrhuvbwrhuthb";
            Fandom fandom = new Fandom(); 
            fandom.EnName = name;
            Assert.True(name.Length>5 && name.Length < 50);
        }
        [Test]
        public void TFandomRu()
        {
            string name = "тлпитдполитощиткпщиуеоои";
            Fandom fandom = new Fandom();
            fandom.EnName = name;
            Assert.True(name.Length > 5 && name.Length < 50);
        }
        [Test]
        public void TGenreEn()
        {
            string name = "gbjobrrghuorbgouhrbgouhbgohubgrhuvbwrhuth";
            Genre genre = new Genre();
            genre.EnName = name;
            Assert.True(name.Length > 5 && name.Length < 50);
        }
        [Test]
        public void TGenreRu()
        {
            string name = "тлпитдполитощиткпщиуеоои";
            Genre genre = new Genre();
            genre.EnName = name;
            Assert.True(name.Length > 5 && name.Length < 50);
        }
        [Test]
        public void TTag()
        {
            string tag = "ehbrbhshrhr";
            Tag tag2 = new Tag();
            tag2.text = tag;
            Assert.True(tag.Length < 25);
        }
        [Test]
        public void TUserLog()
        {
            UserLog userLog = new UserLog();
            string name = "Afnvefnb";
            var password = "rlvbe765veb";
            userLog.Password = password;
            userLog.name=name;
            Assert.True(name.Length < 25 && password.Length < 25);
        }
    }
}