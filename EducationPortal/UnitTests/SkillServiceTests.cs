using Application.DTO;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestClass]
   public  class SkillServiceTests
    {
        Mock<IAutoMapperBLConfiguration> mapper;
        [TestInitialize]
        public void SetupInterfaces()
        {
            mapper = new Mock<IAutoMapperBLConfiguration>();
            mapper.Setup(i => i.GetMapper())
                .Returns(
                    new MapperConfiguration(i =>
                    {
                        i.CreateMap<SkillDTO, Skill>();
                        i.CreateMap<Skill, SkillDTO>();

                        i.CreateMap<CourseDTO, Course>();
                        i.CreateMap<Course, CourseDTO>();

                        i.CreateMap<User, UserDTO>();
                        i.CreateMap<UserDTO, User>();
                    }).CreateMapper());
        }

        [TestMethod]
        public void GetAllSkillsOfCourse_ExistData_ReturnTwoSkills()
        {
            Mock<IEntitiesRepository> repository = new Mock<IEntitiesRepository>();

            repository.Setup(i => i.GetAllBy<CompositionSkillUser>(It.IsAny<Predicate<CompositionSkillUser>>()))
                .Returns(new List<CompositionSkillUser>() { 
                    new CompositionSkillUser {SkillId =1},
                     new CompositionSkillUser {SkillId =2}
                });
            repository.Setup(i => i.GetAllBy<Skill>(It.IsAny<Predicate<Skill>>()))
                .Returns(new List<Skill>() {
                    new Skill {Id =1},
                     new Skill {Id =2}
                });
            var service = new SkillService(repository.Object, mapper.Object);
            var skills = service.GetAllSkillsOfCourse(It.IsAny<int>());

            Assert.AreEqual(2, skills.Count());
        }

        [TestMethod]
        public void GetAllSkillsOfCourse_NotExistData_ReturnEmptyList()
        {
            Mock<IEntitiesRepository> repository = new Mock<IEntitiesRepository>();

            repository.Setup(i => i.GetAllBy<CompositionSkillUser>(It.IsAny<Predicate<CompositionSkillUser>>()))
                .Returns((List<CompositionSkillUser>)null);
            repository.Setup(i => i.GetAllBy<Skill>(It.IsAny<Predicate<Skill>>()))
                .Returns((List<Skill>)null);
            var service = new SkillService(repository.Object, mapper.Object);
            var skills = service.GetAllSkillsOfCourse(It.IsAny<int>());

            Assert.AreEqual(0, skills.Count());
        }

        [TestMethod]
        public void GetAllSkillsOfUser_ExistData_ReturnTwoSkills()
        {
            Mock<IEntitiesRepository> repository = new Mock<IEntitiesRepository>();

            repository.Setup(i => i.GetAllBy<CompositionSkillUser>(It.IsAny<Predicate<CompositionSkillUser>>()))
                .Returns(new List<CompositionSkillUser>() {
                    new CompositionSkillUser {SkillId =1},
                     new CompositionSkillUser {SkillId =2}
                });
            repository.Setup(i => i.GetAllBy<Skill>(It.IsAny<Predicate<Skill>>()))
                .Returns(new List<Skill>() {
                    new Skill {Id =1},
                     new Skill {Id =2}
                });
            var service = new SkillService(repository.Object, mapper.Object);
            var skills = service.GetAllSkillsOfUser(It.IsAny<int>());

            Assert.AreEqual(2, skills.Count());
            Assert.AreEqual(1, skills.First().Id);
            Assert.AreEqual(2, skills.Last().Id);
        }

        [TestMethod]
        public void GetAllSkillsOfUser_NotExistData_ReturnNull()
        {
            Mock<IEntitiesRepository> repository = new Mock<IEntitiesRepository>();

            repository.Setup(i => i.GetAllBy<CompositionSkillUser>(It.IsAny<Predicate<CompositionSkillUser>>()))
                .Returns((List<CompositionSkillUser>)null);
            repository.Setup(i => i.GetAllBy<Skill>(It.IsAny<Predicate<Skill>>()))
                .Returns((List<Skill>)null);
            var service = new SkillService(repository.Object, mapper.Object);
            var skills = service.GetAllSkillsOfUser(It.IsAny<int>());

            Assert.AreEqual(null, skills);
        }
    }
    
}
