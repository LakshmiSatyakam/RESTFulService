using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTFulService.Controllers;
using RESTFulService.Models.Members.DTO;
using RESTFulService.Models.Members.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace RESTFulServiceUnitTest
{
    /// <summary>
    /// Unit test class for MembersController
    /// </summary>
    public class MembersControllerUnitTest
    {
        #region Private fields
        Mock<IMemberOperations> _mockMemberOperations = new Mock<IMemberOperations>();
        MembersController _membersController;

        #endregion

        #region Constructor
        public MembersControllerUnitTest()
        {
            _membersController = new MembersController(_mockMemberOperations.Object);
        }
        #endregion

        #region Test methods
        [Fact]
        public void Constructor_Test()
        {
            MembersController controller = new MembersController(_mockMemberOperations.Object);
            Assert.NotNull(controller);
        }

        [Fact]
        public void Get_Members_NoRecords_Test()
        {
            _mockMemberOperations.Setup(x => x.GetMembers()).Returns(new List<Member>());

            IActionResult result = _membersController.GetMembers();
            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.NotEmpty(((JsonResult)result).Value.ToString());
            Assert.True((result as JsonResult).Value.ToString() == "No members found");
        }

        [Fact]
        public void Get_Members_Exception_Test()
        {
            _mockMemberOperations.Setup(x => x.GetMembers()).Throws(new Exception("Could not connect to DB"));

            IActionResult result = _membersController.GetMembers();
            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((result as BadRequestObjectResult).Value.ToString() == "Could not connect to DB");
        }

        [Fact]
        public void Get_Members_Test()
        {
            IList<Member> list = new List<Member>();
            list.Add(new Member { Id = 1, FirstName = "test1", LastName = "test", DateOfBirth = new DateTime(1980, 01, 01), Email = "test@gmail.com" });
            list.Add(new Member { Id = 2, FirstName = "test2", LastName = "test", DateOfBirth = new DateTime(1960, 01, 01), Email = "test@gmail.com" });
            list.Add(new Member { Id = 3, FirstName = "test3", LastName = "test", DateOfBirth = new DateTime(1901, 01, 01), Email = "test@gmail.com" });
            _mockMemberOperations.Setup(x => x.GetMembers()).Returns(list);

            IActionResult result = _membersController.GetMembers();
            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.True(((result as JsonResult).Value as IOrderedEnumerable<Member>).Count() == 3);
            Assert.True(((result as JsonResult).Value as IOrderedEnumerable<Member>).First().FirstName == "test3");
        }

        [Fact]
        public void Add_Members_Null_Input_Test()
        {
            IActionResult result = _membersController.AddMember(null);
            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
        }

        [Fact]
        public void Add_Members_Invalid_Input_Test()
        {
            Member member = new Member();
            MimicModelStateValidation(_membersController, member);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 3);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["FirstName"] as string[];
            Assert.True(error[0] == "FirstName should not be empty");

            error = (((BadRequestObjectResult)result).Value as SerializableError)["LastName"] as string[];
            Assert.True(error[0] == "LastName should not be empty");

            error = (((BadRequestObjectResult)result).Value as SerializableError)["Email"] as string[];
            Assert.True(error[0] == "Email should not be empty");
        }

        [Fact]
        public void Add_Members_Null_FirstNameInput_Test()
        {
            Member member = new Member();
            member.LastName = "test";
            member.Email = "test@gmail.com";
            MimicModelStateValidation(_membersController, member);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 1);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["FirstName"] as string[];
            Assert.True(error[0] == "FirstName should not be empty");
        }

        [Fact]
        public void Add_Members_Null_LastNameInput_Test()
        {
            Member member = new Member();
            member.FirstName = "test";
            member.Email = "test@gmail.com";
            MimicModelStateValidation(_membersController, member);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 1);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["LastName"] as string[];
            Assert.True(error[0] == "LastName should not be empty");
        }

        [Fact]
        public void Add_Members_Null_EmailInput_Test()
        {
            Member member = new Member();
            member.FirstName = "test";
            member.LastName = "test";
            MimicModelStateValidation(_membersController, member);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True((((BadRequestObjectResult)result).Value as SerializableError).Count() == 1);
            string[] error = (((BadRequestObjectResult)result).Value as SerializableError)["Email"] as string[];
            Assert.True(error[0] == "Email should not be empty");
        }

        [Fact]
        public void Add_Members_Failure_Test()
        {
            Member member = new Member();
            member.FirstName = "test";
            member.LastName = "test";
            member.Email = "tst@gmail.com";

            _mockMemberOperations.Setup(x => x.AddMember(member)).Returns(false);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True(((BadRequestObjectResult)result).Value.ToString() == "Member creation failed!");
        }

        [Fact]
        public void Add_Members_Success_Test()
        {
            Member member = new Member();
            member.FirstName = "test";
            member.LastName = "test";
            member.Email = "tst@gmail.com";

            _mockMemberOperations.Setup(x => x.AddMember(member)).Returns(true);

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(JsonResult));
            Assert.True(((JsonResult)result).Value.ToString() == "Member created successfully");
        }

        [Fact]
        public void Add_Members_Exception_Test()
        {
            Member member = new Member();
            member.FirstName = "test";
            member.LastName = "test";
            member.Email = "tst@gmail.com";

            _mockMemberOperations.Setup(x => x.AddMember(member)).Throws(new Exception("DB Connection could not be established"));

            IActionResult result = _membersController.AddMember(member);

            Assert.True(result.GetType() == typeof(BadRequestObjectResult));
            Assert.True(((BadRequestObjectResult)result).Value.ToString() == "DB Connection could not be established");
        }
        #endregion
        
        #region Private methods

        /// <summary>
        /// Mimics the model state validation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">The control.</param>
        /// <param name="clazz">The clazz.</param>
        void MimicModelStateValidation<T>(Controller control, T clazz) where T : class
        {
            var validationContext = new ValidationContext(clazz, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(clazz, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                control.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        #endregion
    }
}
