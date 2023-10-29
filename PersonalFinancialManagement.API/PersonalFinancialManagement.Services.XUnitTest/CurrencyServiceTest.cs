using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Mapper;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.BaseRepository;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.XUnitTest.SetupMock;
using Xunit;

namespace PersonalFinancialManagement.Services.XUnitTest
{
    public class CurrencyServiceTest
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IUnitOffWork<ApplicationDbContext>> _unitOffWork;
        protected readonly Mock<ILogger<CurrencyService>> _logger;
        public CurrencyServiceTest()
        {
            _unitOffWork = new Mock<IUnitOffWork<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CurrencyService>>();
            if (_mapper != null) return;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task AuthenticationService_GetByIdAsync_IdNotZeroOrNull_ThrowException(string id)
        {
            _unitOffWork.Setup(e => e.Repository<Currency, Guid>().GetNoTrackingEntities()).Returns<IQueryable<Currency>>(null);

            var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => currencyService.GetByIdAsync(It.IsAny<Guid>()));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task AuthenticationService_GetByIdAsync_IdNotZeroOrNull_SuccessAny(int id)
        {
            var data = new List<Currency>
            {
                new()
                {
                    Code = "1",
                    Name = "Name"
                },
                new()
                {
                    Code = "2",
                    Name = "Name2"
                }
            }.AsAsyncQueryable();
            var currenciesRepository = new Mock<IBaseRepository<Currency, Guid>>();
            currenciesRepository.Setup(e => e.GetNoTrackingEntities()).Returns(data);
            _unitOffWork.Setup(e => e.Repository<Currency, Guid>()).Returns(currenciesRepository.Object);

            var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);
            var result = await currencyService.GetByIdAsync(It.IsAny<Guid>());
            Assert.NotNull(result);
        }
        //[Theory]
        //[InlineData(1)]
        //public async Task AuthenticationService_GetByIdAsync_IdNotExist_Empty(string id)
        //{
        //    var data = new List<Currency>
        //    {
        //        new Currency()
        //        {
        //            Code = "1",
        //            Name = "Name"
        //        }
        //    }.AsAsyncQueryable();
        //    var currencysRepository = new Mock<IBaseRepository<Currency, Guid>>();
        //    currencysRepository.Setup(e => e.GetNoTrackingEntities()).Returns(data);
        //    _unitOffWork.Setup(e => e.Repository<Currency, Guid>()).Returns(currencysRepository.Object);

        //    var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);
        //    var result = await currencyService.GetByIdAsync(Guid.Parse(id));
        //    Assert.NotNull(result);
        //}
        //[Fact]
        //public async Task AuthenticationService_GetByIdAsync_IdZeroOrNull_Empty()
        //{
        //    var data = new List<Currency>
        //    {
        //        new Currency()
        //        {
        //            Code = "1",
        //            Name = "Name"
        //        }
        //    }.AsAsyncQueryable();
        //    var currencysRepository = new Mock<IBaseRepository<Currency, Guid>>();
        //    currencysRepository.Setup(e => e.GetNoTrackingEntities()).Returns(data);
        //    _unitOffWork.Setup(e => e.Repository<Currency, Guid>()).Returns(currencysRepository.Object);

        //    var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);
        //    var result = await currencyService.GetByIdAsync(It.IsAny<Guid>());
        //    Assert.NotNull(result);
        //}
        //[Fact]
        //public async Task AuthenticationService_GetByIdAsync_IdExist_Success()
        //{
        //    var data = new List<Currency>
        //    {
        //        new Currency()
        //        {
        //            Code = "1",
        //            Name = "Name"
        //        }
        //    }.AsAsyncQueryable();
        //    var currencysRepository = new Mock<IBaseRepository<Currency, Guid>>();
        //    currencysRepository.Setup(e => e.GetNoTrackingEntities()).Returns(data);
        //    _unitOffWork.Setup(e => e.Repository<Currency, Guid>()).Returns(currencysRepository.Object);

        //    var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);
        //    var result = await currencyService.GetByIdAsync(1, It.IsAny<string>());
        //    Assert.NotEmpty(result);
        //}
        //[Fact]
        //public async Task AuthenticationService_GetAllDTOAsync_NotEmpty()
        //{
        //    Builder<Currency>.
        //        .CreateListOfSize(10)
        //        .All()
        //        .HaveLongTitles()
        //        .Build();
        //    var data = new List<Currency>
        //    {
        //        new Currency()
        //        {
        //            Code = "1",
        //            Name = "Name"
        //        }
        //    }.AsAsyncQueryable();
        //    var currencysRepository = new Mock<IBaseRepository<Currency, Guid>>();
        //    currencysRepository.Setup(e => e.GetNoTrackingEntities()).Returns(data);
        //    _unitOffWork.Setup(e => e.Repository<Currency, Guid>()).Returns(currencysRepository.Object);
        //    var currencyService = new CurrencyService(_mapper, _unitOffWork.Object, _logger.Object);
        //    var result = await currencyService.GetAllDTOAsync();
        //    Assert.True(data.Count() == result.Count());
        //}
    }
}
