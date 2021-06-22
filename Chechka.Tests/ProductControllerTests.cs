using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Chechka.Controllers;
using Chechka.DAL.Entities;
using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.EntityFrameworkCore;
using Chechka.DAL.Data;

namespace Chechka.Tests
{
    public class ProductControllerTests
    {
        //Lb8.4.3.2{
        DbContextOptions<ApplicationDbContext> _options;
        public ProductControllerTests()
        {
            _options =

            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;
        }
        //Lb8.4.3.2}

        //Lb6.4.2.3{
        [Theory]

        //Lb64.4.1--
        //[InlineData(1, 3, 1)] // 1-я страница, кол. объектов 3, id первого объекта 1
        //[InlineData(2, 2, 4)] // 2-я страница, кол. объектов 2, id первого объекта 4
        //Lb64.4.1--

        //Lb64.4.1{
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        //Lb64.4.1}

        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            //Контекст контроллера

            //Lb8.4.3.2{
            var controllerContext = new ControllerContext();
            //Lb8.4.3.2}
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;

            ////Lb8.4.3.2--
            //var controller = new ProductController();
            ////Lb8.4.3.2--

            //Lb64.4.1--
            //controller._cmputerParts = new List<ComputerPart>
            //{
            //new ComputerPart{ ComputerPartId=1},
            //new ComputerPart{ ComputerPartId=2},
            //new ComputerPart{ ComputerPartId=3},
            //new ComputerPart{ ComputerPartId=4},
            //new ComputerPart{ ComputerPartId=5},
            //new ComputerPart{ ComputerPartId=6}
            //};
            //Lb64.4.1--


            ////Lb64.4.1{
            //////Lb8.4.3.2--
            //controller._computerParts = TestData.GetComputerPartsList();
            //////Lb8.4.3.2--
            ////Lb64.4.1}

            //Lb8.4.3.2{
            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                // создать объект класса контроллера
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                //Lb8.4.3.2}

                // Act
                //Lb6.4.6.3--
                //var result = controller.Index(page) as ViewResult;

                //Lb6.4.6.3{
                var result = controller.Index(pageNo: page, group: null) as ViewResult;
                //Lb6.4.6.3}

                var model = result?.Model as List<ComputerPart>;
                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].ComputerPartId);
            }
            // удалить базу данных из памяти
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
        //Lb6.4.2.3}


        //Lb6.4.7.3{
        [Fact]
        public void ControllerSelectGroup()
        {
            // arrange
            //Lb7.4.4{
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());

            controllerContext.HttpContext = moqHttpContext.Object;

            //Lb8.4.3{
            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);

            }
            //Lb8.4.3}

            //Lb8.4.3--
                //var controller = new ProductController(){ ControllerContext = controllerContext };
                ////Lb7.4.4}

                //// Arrange - подготовка исходных данных

                ////Lb7.4.4--
                ////var controller = new ProductController();
                ////Lb7.4.4--

                //var data = TestData.GetComputerPartsList();
                //controller._computerParts = data;

                //var comparer = Comparer<ComputerPart>.GetComparer((d1, d2) => d1.ComputerPartId.Equals(d2.ComputerPartId));

                //// Act - выполнение теста
                //var result = controller.Index(2) as ViewResult;
                //var model = result.Model as List<ComputerPart>;

                //// Assert - проверка того, что результат соответствует ожиданиям

                //Assert.Equal(1, model.Count);
                //Assert.Equal(data[1], model[0], comparer);
            //Lb8.4.3--

            //Lb8.4.3{
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };

                var comparer = Comparer<ComputerPart>.GetComparer((d1, d2) =>
                d1.ComputerPartId.Equals(d2.ComputerPartId));
                // act
                var result = controller.Index(2) as ViewResult;
                var model = result.Model as List<ComputerPart>;
                // assert
                Assert.Equal(2, model.Count);
                Assert.Equal(context.ComputerParts
                .ToArrayAsync()
                .GetAwaiter()
                .GetResult()[2], model[0], comparer);
            }
            //Lb8.4.3}
        }
        //Lb6.4.7.3}
    }
}
