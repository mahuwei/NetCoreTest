using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Domain;
using Project.Web2.Controllers;
using Xunit;

namespace Project.Web2.Test {
    public class BusinessControllerTest {
        public BusinessControllerTest() {
            _fakeMediator = new Fake<IMediator>();
        }

        private readonly Fake<IMediator> _fakeMediator;

        [Fact]
        public async void PostTestError() {
            //var request = new Business();
            //var response = new Response();
            //response.AddError("名称不能为空");
            //response.AddError("编号不能为空");
            //var taskResponse = Task.FromResult(response);
            //_fakeMediator.CallsTo(d => d.Send(request, CancellationToken.None))
            //    .Returns(taskResponse);
            //var controller = new BusinessController(_fakeMediator.FakedObject);
            //var result = await controller.Post(request);
            //Assert.IsType<BadRequestObjectResult>(result);
            //var br = (BadRequestObjectResult) result;
            //var errors = (IEnumerable<string>) br.Value;
            //Assert.Equal(2,errors.Count());   
        }
    }
}