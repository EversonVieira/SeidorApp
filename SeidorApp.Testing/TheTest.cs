using BaseCore.DataBase.Factory;
using BaseCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SeidorApp.Core.Business;
using SeidorApp.Core.DataBase;
using SeidorApp.Core.Models;
using System.Data.Common;

namespace SeidorApp.Testing
{
    [TestFixture]
    public class TheTest
    {
        protected IServiceCollection ServiceCollection { get; set; } = new ServiceCollection();
        protected IServiceProvider ServiceProvider { get; set; }
        private ILoggerFactory _loggerFactory;

        private IDBConnectionFactory _connectionFactory;
        private CpfBusiness _cpfBusiness;



        [SetUp]
        public void Setup()
        {
            if (!File.Exists("SeidorTest.db"))
            {
                File.Create("SeidorTest.db");
            }

            ServiceCollection.AddLogging();
            ServiceCollection.AddSingleton<IDBConnectionFactory, SQLiteConnectionFactory>(x => new SQLiteConnectionFactory("Data Source= SeidorTest.db"));
            ServiceCollection.AddSingleton<CpfBusiness>();

            
            this.ServiceProvider = ServiceCollection.BuildServiceProvider();
            this._loggerFactory = this.ServiceProvider.GetService<ILoggerFactory>();
            this._connectionFactory = this.ServiceProvider.GetService<IDBConnectionFactory>();
            SqliteDataBaseIntegrityService.ValidateIntegrityAndBuildDB(string.Empty, "Data Source= SeidorTest.db");



            _cpfBusiness = ServiceProvider.GetService<CpfBusiness>();;
        }

        [TearDown]
        public void TearDown()
        {
            ClearDataBase();
        }

        #region Insert

        [Test]
        public async Task InsertSuccess()
        {
            var response = _cpfBusiness.Insert(new Core.Models.Cpf()
            {
                OwnerName = "Teste",
                Document = "112.123.123-33",
                IsBlocked = true,
                CreatedBy = "Teste",
                CreatedOn = DateTime.Now,
                ModifiedBy = "Teste",
                ModifiedOn = DateTime.Now,
            });

            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public async Task InsertFail()
        {
            var response = _cpfBusiness.Insert(new Core.Models.Cpf()
            {
                OwnerName = string.Empty,
                Document = string.Empty
            });

            Assert.IsTrue(response.HasValidationMessages);
        }
        #endregion

        #region Update

        [Test]
        public async Task UpdateSuccess()
        {
            var response = _cpfBusiness.Insert(new Core.Models.Cpf()
            {
                Id = 1,
                OwnerName = "teste",
                Document = "112.123.123-33"
            });

            Assert.IsTrue(response.HasAnyMessages);
        }

        [Test]
        public async Task UpdateFail()
        {
            var response = _cpfBusiness.Insert(new Core.Models.Cpf()
            {
                Id = 0,
                OwnerName = string.Empty,
                Document = string.Empty
            });

            Assert.IsTrue(response.HasAnyMessages);
        }
        #endregion

        #region Select 

        [Test]
        public async Task FindAllSuccess()
        {
            var response = _cpfBusiness.FindByRequest(new Request());

            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public async Task FindByDocumentSuccess()
        {
            var response = _cpfBusiness.FindByDocument("112.123.123-33");

            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public async Task FindByDocumentFailure()
        {
            var response = _cpfBusiness.FindByDocument("112.123.33");

            Assert.IsFalse(response.IsValid);
        }

        [Test]
        public async Task FindByIsBlockedSuccess()
        {
            var response = _cpfBusiness.FindByBlockStatus(true);

            Assert.IsTrue(response.IsValid);
        }
        #endregion

        #region Delete

        [Test]
        public async Task DeleteSuccess()
        {
            var response = _cpfBusiness.Delete(new Cpf
            {
                Id = 1
            });

            Assert.IsTrue(response.IsValid);
        }

        [Test]
        public async Task DeleteFail()
        {
            var response = _cpfBusiness.Delete(new Cpf
            {
                Id = 0
            });

            Assert.IsFalse(response.IsValid);
        }
        #endregion

        private void ClearDataBase()
        {
            string cmd = "DELETE FROM CPF";

            _connectionFactory.GetConnection().Open();
            using (DbCommand comm = _connectionFactory.GetConnection().CreateCommand())
            {
                comm.CommandText = cmd;
                comm.ExecuteNonQuery();
            }
            _connectionFactory.GetConnection().Close();

        }

    }
}