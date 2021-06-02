using Application.Interfaces;
using Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Domain;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;
using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces.IServices;

namespace UnitTests
{
    [TestClass]
    public class CoursePassingServiceTests
    {
        Mock<IEntitiesRepository> repository;
        Mock<IServiceUser> serviceUser;
        Mock<IServiceCourse> serviceCourse;
        Mock<IServiceEntities<MaterialDTO>> serviceMaterial;

        [TestInitialize]
        public void SetupInterfaces()
        {
            repository = new Mock<IEntitiesRepository>();
            serviceCourse = new Mock<IServiceCourse>();
            serviceMaterial = new Mock<IServiceEntities<MaterialDTO>>();
            serviceUser = new Mock<IServiceUser>();
        } 

        [TestMethod]
        public void ChooseCourse_InvalidUserId_ReturnFalse()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns(new CourseDTO());
            serviceUser.Setup(i => i.GetBy(It.IsAny<Predicate<UserDTO>>())).Returns( (UserDTO)null);
            repository.Setup(i => i.Create<CompositionPassedMaterial>(It.IsAny<CompositionPassedMaterial>())).Returns(true);
            ICoursePassingService service = new CoursePassingService(repository.Object,serviceMaterial.Object,serviceCourse.Object,serviceUser.Object);
            
            var expected = false;

            var result = service.ChooseCourse(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ChooseCourse_InvalidCourseId_ReturnFalse()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns((CourseDTO)null);
            serviceUser.Setup(i => i.GetBy(It.IsAny<Predicate<UserDTO>>())).Returns((new UserDTO()));
            repository.Setup(i => i.Create<CompositionPassedMaterial>(It.IsAny<CompositionPassedMaterial>())).Returns(true);
            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);
           
            var expected = false;

            var result = service.ChooseCourse(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ChooseCourse_RightCourseIdAndUserId_ReturnTrue()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetById(It.IsAny<int>())).Returns(new CourseDTO());
            serviceUser.Setup(i => i.GetById(It.IsAny<int>())).Returns((new UserDTO() ));
            repository.Setup(i => i.Create<CompositionPassedMaterial>(It.IsAny<CompositionPassedMaterial>())).Returns(true);

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var expected = true;

            var result = service.ChooseCourse(1, 4);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_InvalidCourseIdAndUserId_ReturnsNull()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns((CourseDTO)null);
            serviceUser.Setup(i => i.GetBy(It.IsAny<Predicate<UserDTO>>())).Returns(((UserDTO)null));
            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>())).Returns(new CompositionPassedMaterial { });
            object expected = null;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourse(-1, -1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_RightCourseIdAndUserIdButUserDontTakeCourse_ReturnsNull()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns(new CourseDTO());
            serviceUser.Setup(i => i.GetBy(It.IsAny<Predicate<UserDTO>>())).Returns((new UserDTO()));
            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>())).Returns( (CompositionPassedMaterial)null);
            object expected = null;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourse(1, 1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProgressCourse_RightCourseIdAndUserId_ReturnsCourseProgressDTO()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetById(It.IsAny<int>())).Returns(new CourseDTO() 
            { 
                Id = 1, 
                Materials = new List<MaterialDTO> { 
                    new VideoMaterialDTO() {Id = 1},
                    new ArticleMaterialDTO(){Id = 2},
                    new ArticleMaterialDTO(){Id = 3},
                    new BookMaterialDTO(){Id = 5},
                } 
            });
            serviceUser.Setup(i => i.GetById(It.IsAny<int>())).Returns((new UserDTO()));
            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new CompositionPassedMaterial());
            repository.Setup(i => i.GetAllBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new List<CompositionPassedMaterial>() { 
                            new CompositionPassedMaterial { CourseId = 1, UserId = 2, MaterialsId = new List<int> { 1, 2,3 } },
                            new CompositionPassedMaterial { CourseId = 2, UserId = 2, MaterialsId = new List<int> {  2,4 } },
                    });

            CourseProgressDTO expected = new CourseProgressDTO()
            {
                Id = 1,
                MaterialsNotPassed = new List<MaterialDTO> { new BookMaterialDTO() { Id = 5 }},
                MaterialsPassed = new List<MaterialDTO>
                {
                    new VideoMaterialDTO() {Id = 1},
                    new ArticleMaterialDTO(){Id = 2},
                    new ArticleMaterialDTO(){Id = 3}
                }

            };

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourse(1, 2);

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.MaterialsPassed.Count, result.MaterialsPassed.Count);
            Assert.AreEqual(expected.MaterialsNotPassed.Count, result.MaterialsNotPassed.Count);
        }

        [TestMethod]
        public void GetProgressCourses_UserDontHaveAny_ReturnsEmptyArrayOfCourseProgressDTO()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            repository.Setup(i => i.GetAllBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new List<CompositionPassedMaterial>() {
                    });

            int expected = 0;
           
            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourses(1);

            Assert.AreEqual(expected, result.Count());
           
        }

        [TestMethod]
        public void GetProgressCourses_UserHaveTwoPassingCourses_ReturnsArrayOfCourseProgressDTOCountTwo()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceUser = new Mock<IServiceUser>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns(new CourseDTO(){ Materials = new List<MaterialDTO>() });
            serviceUser.Setup(i => i.GetBy(It.IsAny<Predicate<UserDTO>>())).Returns((new UserDTO()));
            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new CompositionPassedMaterial() { CourseId = 1, MaterialsId = new List<int>() });
            repository.Setup(i => i.GetAllBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new List<CompositionPassedMaterial>()
                {
                    new CompositionPassedMaterial(){ CourseId =1, MaterialsId = new List<int>()},
                    new CompositionPassedMaterial(){ CourseId =2, MaterialsId = new List<int>()},
                });

            int expected = 2;

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.GetProgressCourses(1);

            Assert.AreEqual(expected, result.Count());

        }

        [TestMethod]
        public void PassMaterial_UserPassedMaterialBefore_ReturnsMaterialWithId5()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceMaterial = new Mock<IServiceEntities<MaterialDTO>>();

            serviceCourse.Setup(i => i.GetBy(It.IsAny<Predicate<CourseDTO>>())).Returns(new CourseDTO() { Materials = new List<MaterialDTO>() });
         
            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new CompositionPassedMaterial() { CourseId = 1, MaterialsId = new List<int>() { 5} });

            serviceMaterial.Setup(i => i.GetById(It.IsAny<int>()))
                .Returns(new VideoMaterialDTO() { Id = 5});

            VideoMaterialDTO expected = new VideoMaterialDTO { Id = 5 }; 

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.PassMaterial(It.IsAny<int>(), It.IsAny<int>(), 5);

            Assert.AreEqual(expected.Id, result.Id );
        }

        [TestMethod]
        public void PassMaterial_UserPassedDidntMaterialId5Before_ReturnsMaterial()
        {
            serviceCourse = new Mock<IServiceCourse>();
            repository = new Mock<IEntitiesRepository>();
            serviceMaterial = new Mock<IServiceEntities<MaterialDTO>>();

            serviceCourse.Setup(i => i.GetById(It.IsAny<int>()))
                .Returns(new CourseDTO() { Materials = new List<MaterialDTO>(), MaterialsId = new List<int>() { 5}, Skills = new List<SkillDTO>()});

            repository.Setup(i => i.GetBy<CompositionPassedMaterial>(It.IsAny<Predicate<CompositionPassedMaterial>>()))
                .Returns(new CompositionPassedMaterial() { CourseId = 1, MaterialsId = new List<int>() {  } });

            serviceMaterial.Setup(i => i.GetById(It.IsAny<int>()))
                .Returns(new VideoMaterialDTO() { Id = 5 });

            VideoMaterialDTO expected = new VideoMaterialDTO { Id = 5 };

            ICoursePassingService service = new CoursePassingService(repository.Object, serviceMaterial.Object, serviceCourse.Object, serviceUser.Object);

            var result = service.PassMaterial(It.IsAny<int>(), It.IsAny<int>(), 5);

            Assert.AreEqual(expected.Id, result.Id);
        }
    }
}
