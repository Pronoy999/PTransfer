using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTransfer.Models;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTransfer_Tests.Utilties {
    [TestClass]
    class JwTHelperTests {
        [TestMethod]
        public void GenerateJWTSuccessfully() {
            Users users = new Users(1, "Pronoy", "Mukherjee", "mukherjee_pronoy@yahoo.in", "+919874045815");
            string jwT = JwTHelper.GenrateJwT(users.ToKeyValuePairs());
            Assert.IsNotNull(jwT);
        }
    }
}
