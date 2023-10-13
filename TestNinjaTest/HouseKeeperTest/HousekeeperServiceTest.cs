using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.HousekeeperHelper;
using TestNinja.Mocking;

namespace TestNinjaTest.HouseKeeperTest
{
    [TestFixture]
    public class HousekeeperServiceTest
    {
        private HousekeeperService _housekeeperService;
        private Mock<IStatementGenerator> _statementGeneratorMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IXtraMessageBox> _xtraMessageBoxMock;
        private Housekeeper _housekeeper;
        private readonly DateTime _statementDate = new DateTime(2023, 10, 11);
        private string _fileName;
        private readonly string _subject = "subject";

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            { Oid = 1, Email = "hello1@gmail.com", FullName = "Patricia", StatementEmailBody = "Statement 1" };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(uow => uow.Query<Housekeeper>())
                .Returns(
                    new List<Housekeeper>()
                    {
                        _housekeeper
                    }.AsQueryable());

            _fileName = "fileName";
            _statementGeneratorMock = new Mock<IStatementGenerator>();
            _statementGeneratorMock
                .Setup(st => st.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() =>_fileName);

            _emailSenderMock = new Mock<IEmailSender>();
            _xtraMessageBoxMock = new Mock<IXtraMessageBox>();

            _housekeeperService = new HousekeeperService(
                unitOfWorkMock.Object,
                _statementGeneratorMock.Object,
                _emailSenderMock.Object,
                _xtraMessageBoxMock.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGeneratorMock.Verify(sta => sta.SaveStatement(
                _housekeeper.Oid,
                _housekeeper.FullName,
                _statementDate));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGeneratorMock.Verify(sta =>
                sta.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), times: Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = string.Empty;

            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGeneratorMock.Verify(sta =>
                sta.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), times: Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenStatementFilenameIsNotNull_CallEmailFile()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        //[TestCase("")]
        //[TestCase(" ")]
        //[TestCase(null)]
        public void SendStatementEmails_WhenStatementFilenameIsNull_DoNotCallEmailFile()
        {
            _fileName = null;

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_WhenEmailSendingFails_ShowMessageBox()
        {
            _emailSenderMock.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Throws<Exception>();

            _housekeeperService.SendStatementEmails(_statementDate);

            _xtraMessageBoxMock.Verify(
                m => m.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }

        private void VerifyEmailSent()
        {
            _emailSenderMock.Verify(em =>
                em.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSenderMock.Verify(em =>
                em.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                times:Times.Never);
        }
    }
}
