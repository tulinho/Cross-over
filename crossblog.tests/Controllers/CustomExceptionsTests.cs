using crossblog.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace crossblog.tests.Controllers
{
    public class CustomExceptionsTests
    {

        [Fact]
        public async Task HttpStatusCodeException_Constructor1()
        {
            var exception = new HttpStatusCodeException(404);
            Assert.Equal(exception.StatusCode, 404);
        }

        [Fact]
        public async Task HttpStatusCodeException_Constructor2()
        {
            var exception = new HttpStatusCodeException(404, "Not Found");
            Assert.Equal(exception.StatusCode, 404);
            Assert.Equal(exception.Message, "Not Found");
        }

        [Fact]
        public async Task HttpStatusCodeException_Constructor()
        {
            var exception = new HttpStatusCodeException(404, new Exception());
            Assert.NotNull(exception);
        }

    }
}
